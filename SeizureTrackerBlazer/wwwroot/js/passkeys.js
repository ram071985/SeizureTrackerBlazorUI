/**
 * Seizure App Biometric Helper (2026)
 * Uses the WebAuthn L3 Standard supported by .NET 10
 */

// 1. REGISTRATION: Creates a new public key and saves it to the device/cloud
export async function registerPasskey(optionsJson) {
    try {
        // Parse the JSON challenge received from the .NET 10 API
        const options = PublicKeyCredential.parseCreationOptionsFromJSON(JSON.parse(optionsJson));

        // Trigger the native "Save Passkey" prompt (FaceID/Fingerprint)
        const credential = await navigator.credentials.create({
            publicKey: options
        });

        // Convert the hardware response back to JSON for the server to verify
        return JSON.stringify(credential.toJSON());
    } catch (err) {
        console.error("Biometric Registration Error:", err);
        throw err;
    }
}

// 2. AUTHENTICATION: Verifies an existing passkey for login
export async function authenticatePasskey(optionsJson) {
    try {
        // Parse the login challenge received from the .NET 10 API
        const options = PublicKeyCredential.parseRequestOptionsFromJSON(JSON.parse(optionsJson));

        // Trigger the native "Login with Biometrics" prompt
        const credential = await navigator.credentials.get({
            publicKey: options,
            mediation: 'optional' // In 2026, this allows "Conditional UI" (autofill)
        });

        // Return the cryptographic assertion to the server
        return JSON.stringify(credential.toJSON());
    } catch (err) {
        console.error("Biometric Login Error:", err);
        throw err;
    }
}

// export async function authenticatePasskeyImmediate(optionsJson) {
//     const options = JSON.parse(optionsJson);
//
//     // In 2026, .NET 10 might send base64url. JS needs these as ArrayBuffers.
//     // (Ensure your helper library or manual conversion is handling this)
//
//     try {
//         const credential = await navigator.credentials.get({
//             publicKey: options,
//             mediation: 'conditional' // This tells the OS to show the prompt automatically
//         });
//
//         // Convert the hardware response to a string for Blazor
//         return JSON.stringify(credential);
//     } catch (err) {
//         // If the user hits 'Cancel', we return null to Blazor
//         console.warn("Biometric mediation failed or was cancelled", err);
//         return null;
//     }
// }
let currentAbortController = null;

export async function authenticatePasskeyImmediate(optionsJson) {
    // 1. Cancel any previous pending request
    if (currentAbortController) {
        currentAbortController.abort("New request started");
    }
    currentAbortController = new AbortController();
    
    const options = JSON.parse(optionsJson);

    // 1. Convert the Challenge string to a binary ArrayBuffer
    if (options.challenge) {
        options.challenge = coerceToArrayBuffer(options.challenge);
    }

    // 2. Convert any existing Credential IDs to binary ArrayBuffers
    if (options.allowCredentials) {
        options.allowCredentials.forEach(c => {
            c.id = coerceToArrayBuffer(c.id);
        });
    }

    try {
        const credential = await navigator.credentials.get({
            publicKey: options,
            mediation: 'conditional',
            signal: currentAbortController.signal
        });

        // 3. Convert the hardware's binary response back to Base64 for .NET
        return JSON.stringify({
            id: credential.id,
            rawId: coerceToBase64(credential.rawId),
            type: credential.type,
            response: {
                authenticatorData: coerceToBase64(credential.response.authenticatorData),
                clientDataJSON: coerceToBase64(credential.response.clientDataJSON),
                signature: coerceToBase64(credential.response.signature),
                userHandle: credential.response.userHandle ? coerceToBase64(credential.response.userHandle) : null
            }
        });
    } catch (err) {
        console.error("Biometric prompt error:", err);
        return null;
    }
}

// HELPER: Decodes Base64/Base64Url to ArrayBuffer
function coerceToArrayBuffer(data) {
    if (typeof data === "string") {
        // Handle standard Base64 and Base64Url (replacing - and _ )
        const binaryString = window.atob(data.replace(/-/g, '+').replace(/_/g, '/'));
        const len = binaryString.length;
        const bytes = new Uint8Array(len);
        for (let i = 0; i < len; i++) {
            bytes[i] = binaryString.charCodeAt(i);
        }
        return bytes.buffer;
    }
    return data;
}

// HELPER: Encodes ArrayBuffer to Base64
function coerceToBase64(buffer) {
    const binary = String.fromCharCode(...new Uint8Array(buffer));
    return window.btoa(binary);
}




/**
 * Seizure App Biometric Helper (2026)
 * Uses the WebAuthn L3 Standard supported by .NET 10
 */

// 1. REGISTRATION: Creates a new public key and saves it to the device/cloud
export async function registerPasskey(optionsJson) {
    try {
        // This will print the raw data to the browser's Console tab
        console.log("WebAuthn Options:", optionsJson);

        // You can also log the type to check if it's a String or an Object
        console.log("Data Type:", typeof optionsJson);
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

let currentAbortController = null;

export async function authenticatePasskeyImmediate(optionsJson) {
    if (window.biometricAbortController) {
        window.biometricAbortController.abort();
    }

    window.biometricAbortController = new AbortController();
    // 1. Cancel any previous pending request
    
    currentAbortController = new AbortController();
    
    const options = PublicKeyCredential.parseRequestOptionsFromJSON(JSON.parse(optionsJson));
    

    try {
        const credential = await navigator.credentials.get({
            publicKey: options,
            signal: window.biometricAbortController.signal
        });

        window.biometricAbortController = null;
        // 1. Capture the client extension results (even if empty)
        const extensionResults = credential.getClientExtensionResults();

        // 2. Add it to the JSON string sent back to Blazor
        return JSON.stringify({
            id: credential.id,
            rawId: coerceToBase64(credential.rawId),
            type: credential.type,
            // .NET 10 requires this property explicitly
            clientExtensionResults: extensionResults,
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
    if (!buffer) return null;

    const binary = String.fromCharCode(...new Uint8Array(buffer));

    // Standard btoa creates "Standard Base64"
    // We must manually convert it to "Base64Url" for the .NET 10 API
    return window.btoa(binary)
        .replace(/\+/g, '-') // Replace plus with dash
        .replace(/\//g, '_') // Replace slash with underscore
        .replace(/=/g, '');  // Remove all padding equal signs
}




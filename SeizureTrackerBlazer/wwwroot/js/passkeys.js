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

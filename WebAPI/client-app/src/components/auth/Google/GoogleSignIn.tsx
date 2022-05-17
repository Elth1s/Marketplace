import { Button } from "@mui/material"
import { useEffect, useRef, useState } from "react"
import { useActions } from "../../../hooks/useActions";
import { useNavigate } from "react-router-dom";
import { ExternalLoginServerError } from "../types";

const GoogleSignIn = () => {
    const [gsiScriptLoaded, setGsiScriptLoaded] = useState(false)
    const { GoogleExternalLogin } = useActions();
    const navigate = useNavigate();
    const divRef = useRef<HTMLDivElement>(null);;

    const handleGoogleSignIn = async (res: CredentialResponse) => {
        if (!res.clientId || !res.credential || !divRef.current) return

        try {
            await GoogleExternalLogin({ provider: "Google", token: res.credential });
            navigate("/");

        } catch (exception) {
            const serverError = exception as ExternalLoginServerError;
            console.log(serverError.error);
        }
    }

    const initializeGsi = () => {

        if (!window.google || gsiScriptLoaded || !divRef.current) return

        setGsiScriptLoaded(true)
        window.google.accounts.id.initialize({
            client_id: "776665906575-0a864tctbrd5t6h6m8j84oktpm75jhng.apps.googleusercontent.com",
            callback: handleGoogleSignIn,
        })
        window.google.accounts.id.renderButton(divRef.current, {
            theme: 'filled_blue',
            size: 'large',
            type: 'icon',
            text: 'signin_with',
        });
    }

    useEffect(() => {
        if (gsiScriptLoaded) return


        const script = document.createElement("script")
        script.src = "https://accounts.google.com/gsi/client"
        script.onload = initializeGsi
        script.async = true
        script.id = "google-client-script"
        document.querySelector("body")?.appendChild(script)

        return () => {
            // Cleanup function that runs when component unmounts
            window.google?.accounts.id.cancel()
            document.getElementById("google-client-script")?.remove()
        }
    }, [handleGoogleSignIn, initializeGsi, divRef.current])

    return <div ref={divRef} />
}

export default GoogleSignIn;
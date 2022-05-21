import { GoogleReCaptchaProvider } from 'react-google-recaptcha-v3';

import SignInPage from "./SignInPage";

const SignIn = () => {
    return (
        <GoogleReCaptchaProvider reCaptchaKey="6LeVndYfAAAAAKY8A3dLGIL06JS5KBAeAqpIzlwX">
            <SignInPage />
        </GoogleReCaptchaProvider>
    )
}

export default SignIn;
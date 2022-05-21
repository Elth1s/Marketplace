import { GoogleReCaptchaProvider } from 'react-google-recaptcha-v3';

import SignUpPage from "./SignUpPage";

const SignUp = () => {
    return (
        <GoogleReCaptchaProvider reCaptchaKey="6LeVndYfAAAAAKY8A3dLGIL06JS5KBAeAqpIzlwX">
            <SignUpPage />
        </GoogleReCaptchaProvider>
    )
}

export default SignUp;
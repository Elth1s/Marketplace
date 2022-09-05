import { FC, useEffect } from 'react';
import { GoogleLogin, GoogleLoginResponse } from 'react-google-login';
import { gapi } from 'gapi-script';
import { useNavigate } from "react-router-dom";
import { useActions } from '../../hooks/useActions';
import { ServerError } from '../../store/types';
import { AuthAvatar } from '../../pages/auth/styled';

import { google } from "../../assets/icons"


const GoogleExternalLogin = () => {
    const { GoogleExternalLogin, AuthDialogChange } = useActions();

    const navigate = useNavigate();

    useEffect(() => {
        const start = () => {
            gapi.client.init({
                clientId: process.env.REACT_APP_GOOGLE_CLIENT_ID,
                scope: 'Profile',
            });
        }
        gapi.load('client:auth2', start);
    }, []);

    const handleGoogleSignIn = async (res: GoogleLoginResponse | any) => {
        try {
            await GoogleExternalLogin({ token: res.tokenId });
            AuthDialogChange();

        } catch (exception) {
            const serverError = exception as ServerError;
        }
    }

    const onLoginFailure = (res: any) => {
        console.log('Login Failed:', res);
    };

    return (
        <GoogleLogin
            clientId={process.env.REACT_APP_GOOGLE_CLIENT_ID as string}
            buttonText="Sign In"
            onSuccess={handleGoogleSignIn}
            onFailure={onLoginFailure}
            cookiePolicy='single_host_origin'
            prompt='select_account'
            render={renderProps => (
                <AuthAvatar onClick={renderProps.onClick} sx={{ cursor: "pointer" }} src={google} />
            )}
        />
    );
}
export default GoogleExternalLogin;
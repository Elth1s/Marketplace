import FacebookLogin from 'react-facebook-login/dist/facebook-login-render-props'
import { ReactFacebookLoginInfo } from 'react-facebook-login';

import { useNavigate } from "react-router-dom";
import { useActions } from '../../hooks/useActions';
import { AuthAvatar } from '../../pages/auth/styled';
import { ServerError } from '../../store/types';

import { facebook } from "../../assets/icons";
import { FC } from 'react';

const FacebookExternalLogin = () => {
    const { FacebookExternalLogin, AuthDialogChange } = useActions();
    const navigate = useNavigate();

    const responseFacebook = async (res: ReactFacebookLoginInfo) => {
        try {
            await FacebookExternalLogin({ token: res.accessToken });
            AuthDialogChange();

        } catch (exception) {
            const serverError = exception as ServerError;
        }
    }

    return (
        <FacebookLogin
            appId={process.env.REACT_APP_FACEBOOK_APP_ID as string}
            callback={responseFacebook}
            render={renderProps => (
                <AuthAvatar onClick={renderProps.onClick} sx={{ cursor: "pointer", marginLeft: "40px", borderRadius: "50%" }} src={facebook} ></AuthAvatar>
            )} />
    );
}
export default FacebookExternalLogin;
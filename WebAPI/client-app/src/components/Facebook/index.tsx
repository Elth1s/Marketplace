import { useEffect } from 'react';
import FacebookLogin from 'react-facebook-login/dist/facebook-login-render-props'
import { ReactFacebookLoginInfo } from 'react-facebook-login';

import { useNavigate } from "react-router-dom";
import { useActions } from '../../hooks/useActions';
import { AuthAvatar } from '../../pages/auth/styled';
import { ServerError } from '../../store/types';

import { facebook } from "../../assets/icons";
const appId = "487664976465559";

function FacebookExternalLogin() {
    const { FacebookExternalLogin } = useActions();
    const navigate = useNavigate();

    useEffect(() => {

    }, []);

    const responseFacebook = async (res: ReactFacebookLoginInfo) => {
        try {
            console.log(res.accessToken)
            await FacebookExternalLogin({ token: res.accessToken });
            navigate("/");

        } catch (exception) {
            const serverError = exception as ServerError;
            console.log(serverError.title);
        }
    }

    return (
        <FacebookLogin
            appId={appId}
            callback={responseFacebook}
            render={renderProps => (
                <AuthAvatar onClick={renderProps.onClick} sx={{ cursor: "pointer", marginX: "40px" }} src={facebook} ></AuthAvatar>
            )} />
    );
}
export default FacebookExternalLogin;
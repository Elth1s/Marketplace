import { Dispatch } from "react"
import jwt_decode from "jwt-decode";

import {
    AuthAction,
    ILoginModel,
    IAuthResponse,
    IRegisterModel,
    AuthActionTypes,
    IExternalLoginModel,
} from "./types";
import { ServerError } from "../../store/types";
import http, { setLocalRefreshToken, setLocalAccessToken } from "../../http_comon"

import { accessToken, refreshToken, emailClaim, roleClaim, mobilePhoneClaim, isEmailExistClaim, photoClaim, nameClaim } from "./constants"

export const AuthDialogChange = () => {
    return (dispatch: Dispatch<AuthAction>) => {
        dispatch({
            type: AuthActionTypes.AUTH_DIALOG_CHANGE
        })
    }
}

export const LoginUser = (data: ILoginModel, reCaptchaToken: string) => {
    return async (dispatch: Dispatch<AuthAction>) => {
        try {
            let response = await http.post<IAuthResponse>('api/Auth/SignIn', { ...data, reCaptchaToken: reCaptchaToken })
            const tokens = response.data;
            setLocalAccessToken(tokens.accessToken);
            setLocalRefreshToken(tokens.refreshToken);

            AuthUser(tokens.accessToken, dispatch);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const RegisterUser = (data: IRegisterModel, reCaptchaToken: string) => {
    return async (dispatch: Dispatch<AuthAction>) => {
        try {
            let response = await http.post<IAuthResponse>('api/Auth/SignUp', { ...data, reCaptchaToken: reCaptchaToken })
            const tokens = response.data;
            setLocalAccessToken(tokens.accessToken);
            setLocalRefreshToken(tokens.refreshToken);

            AuthUser(tokens.accessToken, dispatch);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const LogoutUser = () => {
    return async (dispatch: Dispatch<AuthAction>) => {
        dispatch({ type: AuthActionTypes.AUTH_LOGOUT });
        localStorage.removeItem(accessToken)
        localStorage.removeItem(refreshToken)
    }
}

export const GoogleExternalLogin = (data: IExternalLoginModel) => {
    return async (dispatch: Dispatch<AuthAction>) => {
        try {
            let response = await http.post<IAuthResponse>('api/Auth/GoogleExternalLogin', data)
            const tokens = response.data;
            setLocalAccessToken(tokens.accessToken);
            setLocalRefreshToken(tokens.refreshToken);

            AuthUser(tokens.accessToken, dispatch);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const FacebookExternalLogin = (data: IExternalLoginModel) => {
    return async (dispatch: Dispatch<AuthAction>) => {
        try {
            let response = await http.post<IAuthResponse>('api/Auth/FacebookExternalLogin', data)
            const tokens = response.data;
            setLocalAccessToken(tokens.accessToken);
            setLocalRefreshToken(tokens.refreshToken);

            AuthUser(tokens.accessToken, dispatch);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const AuthUser = (token: string, dispatch: Dispatch<AuthAction>) => {
    const decodedToken = jwt_decode(token) as any;
    dispatch({
        type: AuthActionTypes.AUTH_SUCCESS,
        payload: { name: decodedToken[nameClaim], photo: decodedToken[photoClaim], emailOrPhone: decodedToken[emailClaim] ?? decodedToken[mobilePhoneClaim], isEmailExist: decodedToken[isEmailExistClaim].toLowerCase() == 'true', role: decodedToken[roleClaim] }
    })
}


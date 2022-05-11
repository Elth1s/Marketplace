import { Dispatch } from "react"
import axios, { AxiosError } from "axios";
// import jwt from "jsonwebtoken";
import jwt_decode from "jwt-decode";

import {
    IUser,
    AuthAction,
    ILoginModel,
    IAuthResponse,
    IRegisterModel,
    AuthActionTypes,
    LoginServerError,
    RegisterServerError,
    IExternalLoginModel,
    ExternalLoginServerError
} from "./types";
import http, { setLocalRefreshToken, setLocalAccessToken } from "../../http_comon"

import { accessToken, refreshToken } from "./constants"

export const LoginUser = (data: ILoginModel, reCaptchaToken: string) => {
    return async (dispatch: Dispatch<AuthAction>) => {
        try {
            let response = await http.post<IAuthResponse>('/api/Auth/SignIn', { ...data, reCaptchaToken: reCaptchaToken })
            const tokens = response.data;
            setLocalAccessToken(tokens.accessToken);
            setLocalRefreshToken(tokens.refreshToken);

            AuthUser(tokens.accessToken);
            return Promise.resolve();
        }
        catch (error) {
            if (axios.isAxiosError(error)) {
                const serverError = error as AxiosError<LoginServerError>;
                if (serverError && serverError.response) {
                    serverError.response.data.status = serverError.response.status;
                    return Promise.reject(serverError.response.data);
                }
            }
            return Promise.reject(error)
        }
    }
}

export const RegisterUser = (data: IRegisterModel, reCaptchaToken: string) => {
    return async (dispatch: Dispatch<AuthAction>) => {
        try {
            let response = await http.post<IAuthResponse>('/api/Auth/SignUp', { ...data, reCaptchaToken: reCaptchaToken })
            const tokens = response.data;
            setLocalAccessToken(tokens.accessToken);
            setLocalRefreshToken(tokens.refreshToken);

            AuthUser(tokens.accessToken);
            return Promise.resolve();
        }
        catch (error) {
            if (axios.isAxiosError(error)) {
                const serverError = error as AxiosError<RegisterServerError>;
                if (serverError && serverError.response) {
                    serverError.response.data.status = serverError.response.status;
                    return Promise.reject(serverError.response.data);
                }
            }
            return Promise.reject(error)
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
    return async () => {
        try {
            let response = await http.post<IAuthResponse>('/api/Auth/GoogleExternalLogin', data)
            const tokens = response.data;
            setLocalAccessToken(tokens.accessToken);
            setLocalRefreshToken(tokens.refreshToken);

            AuthUser(tokens.accessToken);
            return Promise.resolve();
        }
        catch (error) {
            if (axios.isAxiosError(error)) {
                const serverError = error as AxiosError<ExternalLoginServerError>;
                if (serverError && serverError.response) {
                    return Promise.reject(serverError.response.data);
                }
            }
            return Promise.reject(error)
        }
    }
}

export const AuthUser = (token: string) => {
    return async (dispatch: Dispatch<AuthAction>) => {
        const user = jwt_decode(token) as IUser;
        dispatch({
            type: AuthActionTypes.AUTH_SUCCESS,
            payload: { id: user.id, firstName: user.firstName, secondName: user.secondName, photo: user.photo, email: user.email, roles: user.roles }
        })
    }
}

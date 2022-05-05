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
    RegisterServerError
} from "./types";
import http from "../../http_comon"

import { accessToken, refreshToken } from "./constants"

export const LoginUser = (data: ILoginModel) => {
    return async (dispatch: Dispatch<AuthAction>) => {
        try {
            let response = await http.post<IAuthResponse>('api/Auth/SignIn', data)
            const tokens = response.data;
            localStorage.setItem(accessToken, tokens.accessToken);
            localStorage.setItem(refreshToken, tokens.refreshToken);

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

export const RegisterUser = (data: IRegisterModel) => {
    return async (dispatch: Dispatch<AuthAction>) => {
        try {
            let response = await http.post<IAuthResponse>('api/Auth/SignUp', data)
            const tokens = response.data;
            localStorage.setItem(accessToken, tokens.accessToken);
            localStorage.setItem(refreshToken, tokens.refreshToken);

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
        localStorage.removeItem("token")
    }
}

export const AuthUser = (token: string) => {
    return async (dispatch: Dispatch<AuthAction>) => {
        const user = jwt_decode(token) as IUser;
        dispatch({
            type: AuthActionTypes.AUTH_SUCCESS,
            payload: { id: user.id, name: user.name, surname: user.surname, photo: user.photo, email: user.email, roles: user.roles }
        })
    }
}

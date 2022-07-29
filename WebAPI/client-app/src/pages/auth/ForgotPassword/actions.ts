import { Dispatch } from "react"
import axios, { AxiosError } from "axios"

import {
    IPhoneCodeRequest,
    IResetChangePassword,
    IResetPasswordEmail,
    IResetPasswordInfo,
    IResetPasswordPhone,
    PasswordResetAction,
    ResetPasswordActionTypes
} from "./types"
import { ServerError } from "../../../store/types"

import http from "../../../http_comon"


export const ResetChangePassword = (data: IResetChangePassword) => {
    return async (dispatch: Dispatch<PasswordResetAction>) => {
        try {
            let response = await http.put(`/api/User/ResetPassword`, data)
            console.log(response)
            dispatch({
                type: ResetPasswordActionTypes.RESET_CHANGE_PASSWORD,
            })
            return Promise.resolve();
        }
        catch (error) {
            if (axios.isAxiosError(error)) {
                const serverError = error as AxiosError<ServerError>;
                if (serverError && serverError.response) {
                    // serverError.response.data. = serverError.response.status;
                    return Promise.reject(serverError.response.data);
                }
            }
            return Promise.reject(error)
        }
    }
}

export const SendResetPasswordByEmail = (data: IResetPasswordEmail) => {
    return async (dispatch: Dispatch<PasswordResetAction>) => {
        try {
            let response = await http.post(`/api/User/ResetPasswordByEmail`, data)
            dispatch({
                type: ResetPasswordActionTypes.RESET_PASSWORD,
            })
            return Promise.resolve();
        }
        catch (error) {
            if (axios.isAxiosError(error)) {
                const serverError = error as AxiosError<ServerError>;
                if (serverError && serverError.response) {
                    return Promise.reject(serverError.response.data);
                }
            }
            return Promise.reject(error)
        }
    }
}

export const SendResetPasswordByPhoneCode = (data: IPhoneCodeRequest) => {
    return async (dispatch: Dispatch<PasswordResetAction>) => {
        try {
            let response = await http.post(`/api/User/ResetPasswordByPhone`, data)
            dispatch({
                type: ResetPasswordActionTypes.RESET_PASSWORD,
            })
            return Promise.resolve();
        }
        catch (error) {
            if (axios.isAxiosError(error)) {
                const serverError = error as AxiosError<ServerError>;
                if (serverError && serverError.response) {
                    return Promise.reject(serverError.response.data);
                }
            }
            return Promise.reject(error)
        }
    }
}

export const ValidateCodeForResetPasswordByPhone = (data: IResetPasswordPhone) => {
    return async (dispatch: Dispatch<PasswordResetAction>) => {
        try {
            let response = await http.post<IResetPasswordInfo>(`/api/User/ValidateResetPasswordPhoneCode`, data)
            return Promise.resolve(response.data);
        }
        catch (error) {
            if (axios.isAxiosError(error)) {
                const serverError = error as AxiosError<ServerError>;
                if (serverError && serverError.response) {
                    return Promise.reject(serverError.response.data);
                }
            }
            return Promise.reject(error)
        }
    }
}
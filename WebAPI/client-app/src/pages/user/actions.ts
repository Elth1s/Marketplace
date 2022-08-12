import axios, { AxiosError } from "axios"
import { Dispatch } from "react"
import http, { baseURL } from "../../http_comon"
import { ServerError } from "../../store/types"
import { AuthUser } from "../auth/actions"
import { AuthAction } from "../auth/types"
import {
    ConfirmEmailActionTypes,
    EmailConfirmAction,
    IConfirmEmail,
    IProfile,
    ProfileAction,
    ProfileActionTypes,
} from "./types"

export const GetProfile = () => {
    return async (dispatch: Dispatch<ProfileAction>) => {
        try {
            let response = await http.get<IProfile>(`api/User/Profile`)

            dispatch({
                type: ProfileActionTypes.GET_PROFILE,
                payload: response.data
            })
            return Promise.resolve();
        }
        catch (error) {
            if (axios.isAxiosError(error)) {
                const serverError = error as AxiosError<ServerError>;
                if (serverError && serverError.response) {
                    serverError.response.data.status = serverError.response.status;
                    return Promise.reject(serverError.response.data);
                }
            }
            return Promise.reject(error)
        }
    }
}

export const UpdateProfile = (data: IProfile) => {
    return async (dispatch: Dispatch<AuthAction>) => {
        try {

            let response = await http.put(`api/User/UpdateProfile`, data)

            // AuthUser(token);
            return Promise.resolve();
        }
        catch (ex) {
            if (axios.isAxiosError(ex)) {
                const serverError = ex as AxiosError<ServerError>;
                if (serverError && serverError.response) {
                    serverError.response.data.status = serverError.response.status;
                    return Promise.reject(serverError.response.data);
                }
            }
            return Promise.reject(ex)
        }
    }
}

export const ConfirmEmail = (data: IConfirmEmail) => {
    return async (dispatch: Dispatch<EmailConfirmAction>) => {
        try {
            let response = await http.put(`/api/User/ConfirmEmail`, data)
            dispatch({
                type: ConfirmEmailActionTypes.CONFIRM_EMAIL,
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

export const SendConfirmEmail = () => {
    return async (dispatch: Dispatch<EmailConfirmAction>) => {
        try {
            let response = await http.post(`/api/User/ConfirmEmail`)
            dispatch({
                type: ConfirmEmailActionTypes.SEND_CONFIRM_EMAIL,
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

export const IsEmailConfirmed = () => {
    return async (dispatch: Dispatch<EmailConfirmAction>) => {
        try {
            let res = await http.get(`/api/User/IsEmailConfirmed`)
            dispatch({
                type: ConfirmEmailActionTypes.IS_EMAIL_CONFIRMED,
                payload: res.data
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
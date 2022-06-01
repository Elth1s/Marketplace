import axios, { AxiosError } from "axios"
import { Dispatch } from "react"
import http, { baseURL } from "../../../http_comon"
import { AuthUser } from "../../auth/actions"
import { AuthAction } from "../../auth/types"
import { IProfile, ProfileAction, ProfileActionTypes, ProfileServerError } from "./types"

export const GetProfile = () => {
    return async (dispatch: Dispatch<ProfileAction>) => {
        try {
            let response = await http.get<IProfile>(`api/User/Profile`)
            if (response.data.photo != "")
                response.data.photo = baseURL + response.data.photo;
            dispatch({
                type: ProfileActionTypes.GET_PROFILE,
                payload: response.data
            })
            return Promise.resolve();
        }
        catch (error) {
            if (axios.isAxiosError(error)) {
                const serverError = error as AxiosError<ProfileServerError>;
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
                const serverError = ex as AxiosError<ProfileServerError>;
                if (serverError && serverError.response) {
                    serverError.response.data.status = serverError.response.status;
                    return Promise.reject(serverError.response.data);
                }
            }
            return Promise.reject(ex)
        }
    }
}
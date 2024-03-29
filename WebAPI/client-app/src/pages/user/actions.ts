import axios, { AxiosError } from "axios"
import { Dispatch } from "react"
import http from "../../http_comon"
import { ServerError } from "../../store/types"
import { AuthUser } from "../auth/actions"
import { AuthAction, AuthActionTypes, IExternalLoginModel } from "../auth/types"
import {
    ConfirmEmailActionTypes,
    EmailConfirmAction,
    GenderAction,
    GenderActionTypes,
    IConfirmEmail,
    IGender,
    IOrderCreate,
    IOrderProducts,
    IOrdersForUser,
    IProfile,
    OrderAction,
    OrderActionTypes,
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

            let response = await http.put(`api/User/UpdateProfile`, { ...data, cityId: data.cityId != "" ? data.cityId : null, genderId: data.genderId != "" ? data.genderId : null, countryId: data.countryId != "" ? data.countryId : null })

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
            let response = await http.get(`/api/User/IsEmailConfirmed`)
            dispatch({
                type: ConfirmEmailActionTypes.IS_EMAIL_CONFIRMED,
                payload: response.data
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

export const GetOrderProducts = () => {
    return async (dispatch: Dispatch<OrderAction>) => {
        try {
            let response = await http.get<Array<IOrderProducts>>(`/api/BasketItem/GetBasketItemsForOrder`)
            dispatch({
                type: OrderActionTypes.GET_ORDER_PRODUCTS,
                payload: response.data
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

export const GetOrderForUser = () => {
    return async (dispatch: Dispatch<OrderAction>) => {
        try {
            let response = await http.get<Array<IOrdersForUser>>(`/api/Order/GetForUser`)
            dispatch({
                type: OrderActionTypes.GET_ORDER_FOR_USER,
                payload: response.data
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

export const GoogleConnect = (data: IExternalLoginModel) => {
    return async (dispatch: Dispatch<ProfileAction>) => {
        try {
            await http.post('api/User/GoogleConnect', data)
            dispatch({
                type: ProfileActionTypes.GOOGLE_CONNECT_SUCCESS,
            })
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const FacebookConnect = (data: IExternalLoginModel) => {
    return async (dispatch: Dispatch<ProfileAction>) => {
        try {
            await http.post('api/User/FacebookConnect', data)
            dispatch({
                type: ProfileActionTypes.FACEBOOK_CONNECT_SUCCESS,
            })
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const CreateOrder = (values: IOrderCreate) => {
    return async () => {
        try {
            let response = await http.post(`/api/Order/CreateOrder`, values)

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

export const CancelOrder = (orderId: number) => {
    return async () => {
        try {
            let response = await http.put(`/api/Order/CancelOrder/${orderId}`)

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

export const GetGenders = () => {
    return async (dispatch: Dispatch<GenderAction>) => {
        try {
            let response = await http.get<Array<IGender>>(`api/Gender/Get`)

            dispatch({
                type: GenderActionTypes.GET_GENDERS,
                payload: response.data
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

export const RemoveProfile = () => {
    return async (dispatch: Dispatch<AuthAction>) => {
        try {
            await http.delete(`/api/User/RemoveProfile`)

            dispatch({
                type: AuthActionTypes.AUTH_LOGOUT
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
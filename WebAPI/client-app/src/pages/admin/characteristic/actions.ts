import { Dispatch } from "react"
import axios, { AxiosError } from "axios"

import http from "../../../http_comon"
import {
    ICharacteristic,
    ICharacteristicInfo,
    CharacteristicAction,
    CharacteristicActionTypes,
    CharacteristicServerError
} from "./types"

export const GetByIdCharacteristic = (id: string | null) => {
    return async (dispatch: Dispatch<CharacteristicAction>) => {
        try {
            let response = await http.get<ICharacteristicInfo>(`api/Characteristic/GetById/${id}`)

            dispatch({
                type: CharacteristicActionTypes.GET_BY_ID_CHARACTERISTIC,
                payload: response.data
            })

            return Promise.resolve();
        } catch (ex) {
            if (axios.isAxiosError(ex)) {
                const serverError = ex as AxiosError<CharacteristicServerError>;
                if (serverError && serverError.response) {
                    serverError.response.data.status = serverError.response.status;
                    return Promise.reject(serverError.response.data);
                }
            }
            return Promise.reject();
        }
    }
}

export const GetCharacteristics = () => {
    return async (dispatch: Dispatch<CharacteristicAction>) => {
        try {
            let response = await http.get<Array<ICharacteristicInfo>>(`api/Characteristic/Get`)

            dispatch({
                type: CharacteristicActionTypes.GET_CHARACTERISTICS,
                payload: response.data
            })

            return Promise.resolve();
        } catch (ex) {
            if (axios.isAxiosError(ex)) {
                const serverError = ex as AxiosError<CharacteristicServerError>;
                if (serverError && serverError.response) {
                    serverError.response.data.status = serverError.response.status;
                    return Promise.reject(serverError.response.data);
                }
            }
            return Promise.reject();
        }
    }
}

export const CreateCharacteristic = (data: ICharacteristic) => {
    return async () => {
        try {
            await http.post("api/Characteristic/Create", data);
            return Promise.resolve();
        } catch (ex) {
            if (axios.isAxiosError(ex)) {
                const serverError = ex as AxiosError<CharacteristicServerError>;
                if (serverError && serverError.response) {
                    serverError.response.data.status = serverError.response.status;
                    return Promise.reject(serverError.response.data);
                }
            }
            return Promise.reject();
        }
    }
}

export const UpdateCharacteristic = (id: number, data: ICharacteristic) => {
    return async () => {
        try {
            await http.put<ICharacteristic>(`api/Characteristic/Update/${id}`, data);
            return Promise.resolve();
        } catch (ex) {
            if (axios.isAxiosError(ex)) {
                const serverError = ex as AxiosError<CharacteristicServerError>;
                if (serverError && serverError.response) {
                    serverError.response.data.status = serverError.response.status;
                    return Promise.reject(serverError.response.data);
                }
            }
            return Promise.reject();
        }
    }
}

export const DeleteCharacteristic = (id: number) => {
    return async () => {
        try {
            await http.delete(`api/Characteristic/Delete/${id}`);
            return Promise.resolve();
        } catch (ex) {
            if (axios.isAxiosError(ex)) {
                const serverError = ex as AxiosError<CharacteristicServerError>;
                if (serverError && serverError.response) {
                    serverError.response.data.status = serverError.response.status;
                    return Promise.reject(serverError.response.data);
                }
            }
            return Promise.reject();
        }
    }
}
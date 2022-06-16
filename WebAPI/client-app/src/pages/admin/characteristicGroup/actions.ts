import { Dispatch } from "react"
import axios, { AxiosError } from "axios"

import http from "../../../http_comon"
import {
    ICharacteristicGroup,
    ICharacteristicGroupInfo,
    CharacteristicGroupAction,
    CharacteristicGroupActionTypes,
    CharacteristicGroupServerError
} from "./types"

export const GetByIdCharacteristicGroup = (id: number) => {
    return async (dispatch: Dispatch<CharacteristicGroupAction>) => {
        try {
            let response = await http.get<ICharacteristicGroupInfo>(`api/CharacteristicGroup/GetById/${id}`)

            dispatch({
                type: CharacteristicGroupActionTypes.GET_BY_ID_CHARACTERISTIC_GROUP,
                payload: response.data
            })

            return Promise.resolve();
        } catch (ex) {
            if (axios.isAxiosError(ex)) {
                const serverError = ex as AxiosError<CharacteristicGroupServerError>;
                if (serverError && serverError.response) {
                    serverError.response.data.status = serverError.response.status;
                    return Promise.reject(serverError.response.data);
                }
            }
            return Promise.reject();
        }
    }
}

export const GetCharacteristicGroups = () => {
    return async (dispatch: Dispatch<CharacteristicGroupAction>) => {
        try {
            let response = await http.get<Array<ICharacteristicGroupInfo>>(`api/CharacteristicGroup/Get`)

            dispatch({
                type: CharacteristicGroupActionTypes.GET_CHARACTERISTIC_GROUPS,
                payload: response.data
            })

            return Promise.resolve();
        } catch (ex) {
            if (axios.isAxiosError(ex)) {
                const serverError = ex as AxiosError<CharacteristicGroupServerError>;
                if (serverError && serverError.response) {
                    serverError.response.data.status = serverError.response.status;
                    return Promise.reject(serverError.response.data);
                }
            }
            return Promise.reject();
        }
    }
}

export const CreateCharacteristicGroup = (data: ICharacteristicGroup) => {
    return async () => {
        try {
            await http.post("api/CharacteristicGroup/Create", data);
            return Promise.resolve();
        } catch (ex) {
            if (axios.isAxiosError(ex)) {
                const serverError = ex as AxiosError<CharacteristicGroupServerError>;
                if (serverError && serverError.response) {
                    serverError.response.data.status = serverError.response.status;
                    return Promise.reject(serverError.response.data);
                }
            }
            return Promise.reject();
        }
    }
}

export const UpdateCharacteristicGroup = (id: number, data: ICharacteristicGroup) => {
    return async () => {
        try {
            await http.put<ICharacteristicGroup>(`api/CharacteristicGroup/Update/${id}`, data);
            return Promise.resolve();
        } catch (ex) {
            if (axios.isAxiosError(ex)) {
                const serverError = ex as AxiosError<CharacteristicGroupServerError>;
                if (serverError && serverError.response) {
                    serverError.response.data.status = serverError.response.status;
                    return Promise.reject(serverError.response.data);
                }
            }
            return Promise.reject();
        }
    }
}

export const DeleteCharacteristicGroup = (id: number) => {
    return async () => {
        try {
            await http.delete(`api/CharacteristicGroup/Delete/${id}`);
            return Promise.resolve();
        } catch (ex) {
            if (axios.isAxiosError(ex)) {
                const serverError = ex as AxiosError<CharacteristicGroupServerError>;
                if (serverError && serverError.response) {
                    serverError.response.data.status = serverError.response.status;
                    return Promise.reject(serverError.response.data);
                }
            }
            return Promise.reject();
        }
    }
}
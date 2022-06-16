import { Dispatch } from "react"

import http from "../../../http_comon"
import {
    ICharacteristic,
    ICharacteristicInfo,
    CharacteristicAction,
    CharacteristicActionTypes,
} from "./types"
import { ServerError } from "../../../store/types"

export const GetByIdCharacteristic = (id: string | null) => {
    return async (dispatch: Dispatch<CharacteristicAction>) => {
        try {
            let response = await http.get<ICharacteristicInfo>(`api/Characteristic/GetById/${id}`)

            dispatch({
                type: CharacteristicActionTypes.GET_BY_ID_CHARACTERISTIC,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
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
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const CreateCharacteristic = (data: ICharacteristic) => {
    return async () => {
        try {
            await http.post("api/Characteristic/Create", data);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const UpdateCharacteristic = (id: number, data: ICharacteristic) => {
    return async () => {
        try {
            await http.put<ICharacteristic>(`api/Characteristic/Update/${id}`, data);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const DeleteCharacteristic = (id: number) => {
    return async () => {
        try {
            await http.delete(`api/Characteristic/Delete/${id}`);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}
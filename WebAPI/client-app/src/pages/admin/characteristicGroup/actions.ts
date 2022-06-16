import { Dispatch } from "react"

import http from "../../../http_comon"
import {
    ICharacteristicGroup,
    ICharacteristicGroupInfo,
    CharacteristicGroupAction,
    CharacteristicGroupActionTypes,
} from "./types"
import { ServerError } from "../../../store/types"

export const GetByIdCharacteristicGroup = (id: string | null) => {
    return async (dispatch: Dispatch<CharacteristicGroupAction>) => {
        try {
            let response = await http.get<ICharacteristicGroupInfo>(`api/CharacteristicGroup/GetById/${id}`)

            dispatch({
                type: CharacteristicGroupActionTypes.GET_BY_ID_CHARACTERISTIC_GROUP,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
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
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const CreateCharacteristicGroup = (data: ICharacteristicGroup) => {
    return async () => {
        try {
            await http.post("api/CharacteristicGroup/Create", data);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const UpdateCharacteristicGroup = (id: number, data: ICharacteristicGroup) => {
    return async () => {
        try {
            await http.put<ICharacteristicGroup>(`api/CharacteristicGroup/Update/${id}`, data);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const DeleteCharacteristicGroup = (id: number) => {
    return async () => {
        try {
            await http.delete(`api/CharacteristicGroup/Delete/${id}`);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}
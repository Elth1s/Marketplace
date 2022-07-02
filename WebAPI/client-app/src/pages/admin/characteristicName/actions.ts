import { Dispatch } from "react"
import qs from "qs"

import http from "../../../http_comon"
import {
    ICharacteristicName,
    ICharacteristicNameInfo,
    CharacteristicNameAction,
    CharacteristicNameActionTypes,
    ISearchCharacteristicNames,
} from "./types"
import { ServerError } from "../../../store/types"

export const GetByIdCharacteristicName = (id: number) => {
    return async (dispatch: Dispatch<CharacteristicNameAction>) => {
        try {
            let response = await http.get<ICharacteristicName>(`api/CharacteristicName/${id}`)

            dispatch({
                type: CharacteristicNameActionTypes.GET_BY_ID_CHARACTERISTIC_NAME,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const SearchCharacteristicNames = (page: number, rowsPerPage: number, name: string, isAscOrder: boolean, orderBy: string) => {
    return async (dispatch: Dispatch<CharacteristicNameAction>) => {
        try {
            let response = await http.get<ISearchCharacteristicNames>(`api/CharacteristicName/Search`, {
                params: {
                    page: page,
                    rowsPerPage: rowsPerPage,
                    name: name,
                    isAscOrder: isAscOrder,
                    orderBy: orderBy
                },
                paramsSerializer: params => {
                    return qs.stringify({ ...params })
                }
            })

            dispatch({
                type: CharacteristicNameActionTypes.SEARCH_CHARACTERISTIC_NAMES,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetCharacteristicNames = () => {
    return async (dispatch: Dispatch<CharacteristicNameAction>) => {
        try {
            let response = await http.get<Array<ICharacteristicNameInfo>>(`api/CharacteristicName/Get`)

            dispatch({
                type: CharacteristicNameActionTypes.GET_CHARACTERISTIC_NAMES,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const CreateCharacteristicName = (data: ICharacteristicName) => {
    return async () => {
        try {
            await http.post("api/CharacteristicName", data);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const UpdateCharacteristicName = (id: number, data: ICharacteristicName) => {
    return async () => {
        try {
            await http.put<ICharacteristicName>(`api/CharacteristicName/${id}`, data);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const DeleteCharacteristicName = (id: number) => {
    return async () => {
        try {
            await http.delete(`api/CharacteristicName/${id}`);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const DeleteCharacteristicNames = (ids: readonly number[]) => {
    return async () => {
        try {
            await http.delete(`api/CharacteristicName`, {
                params: {
                    ids: ids,
                },
                paramsSerializer: params => { return qs.stringify({ ...params }) }
            })
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}
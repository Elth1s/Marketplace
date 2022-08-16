import { Dispatch } from "react"
import qs from "qs"

import http from "../../../http_comon"
import {
    ICharacteristicValue,
    ICharacteristicValueInfo,
    CharacteristicValueAction,
    CharacteristicValueActionTypes,
    ISearchCharacteristicValues,
} from "./types"
import { ServerError } from "../../../store/types"

export const GetByIdCharacteristicValue = (id: number) => {
    return async (dispatch: Dispatch<CharacteristicValueAction>) => {
        try {
            let response = await http.get<ICharacteristicValue>(`api/CharacteristicValue/GetById/${id}`)

            dispatch({
                type: CharacteristicValueActionTypes.GET_BY_ID_CHARACTERISTIC_VALUE,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const SearchCharacteristicValues = (page: number, rowsPerPage: number, name: string, isAscOrder: boolean, orderBy: string, isSeller: boolean) => {
    return async (dispatch: Dispatch<CharacteristicValueAction>) => {
        try {
            let response = await http.get<ISearchCharacteristicValues>(`api/CharacteristicValue/Search`, {
                params: {
                    page: page,
                    rowsPerPage: rowsPerPage,
                    name: name,
                    isAscOrder: isAscOrder,
                    orderBy: orderBy,
                    isSeller: isSeller
                },
                paramsSerializer: params => {
                    return qs.stringify({ ...params })
                }
            })

            dispatch({
                type: CharacteristicValueActionTypes.SEARCH_CHARACTERISTIC_VALUE,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetCharacteristicValues = () => {
    return async (dispatch: Dispatch<CharacteristicValueAction>) => {
        try {
            let response = await http.get<Array<ICharacteristicValueInfo>>(`api/CharacteristicValue/Get`)

            dispatch({
                type: CharacteristicValueActionTypes.GET_CHARACTERISTIC_VALUES,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const CreateCharacteristicValue = (data: ICharacteristicValue) => {
    return async () => {
        try {
            await http.post("api/CharacteristicValue/Create", data);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const UpdateCharacteristicValue = (id: number, data: ICharacteristicValue) => {
    return async () => {
        try {
            await http.put<ICharacteristicValue>(`api/CharacteristicValue/Update/${id}`, data);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const DeleteCharacteristicValue = (id: number) => {
    return async () => {
        try {
            await http.delete(`api/CharacteristicValue/Delete/${id}`);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const DeleteCharacteristicValues = (ids: readonly number[]) => {
    return async () => {
        try {
            await http.delete(`api/CharacteristicValue/Delete`, {
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
import { Dispatch } from "react"
import qs from "qs"

import http from "../../../http_comon"
import {
    ICharacteristicGroup,
    ICharacteristicGroupInfo,
    CharacteristicGroupAction,
    CharacteristicGroupActionTypes,
    ISearchCharacteristicGroups,
} from "./types"
import { ServerError } from "../../../store/types"

export const SearchCharacteristicGroups = (page: number, rowsPerPage: number, name: string, isAscOrder: boolean, orderBy: string) => {
    return async (dispatch: Dispatch<CharacteristicGroupAction>) => {
        try {
            let response = await http.get<ISearchCharacteristicGroups>(`api/CharacteristicGroup/Search`, {
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
                type: CharacteristicGroupActionTypes.SEARCH_CHARACTERISTIC_GROUPS,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetByIdCharacteristicGroup = (id: number) => {
    return async (dispatch: Dispatch<CharacteristicGroupAction>) => {
        try {
            let response = await http.get<ICharacteristicGroupInfo>(`api/CharacteristicGroup/${id}`)

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
            let response = await http.get<Array<ICharacteristicGroupInfo>>(`api/CharacteristicGroup`)

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
            await http.post("api/CharacteristicGroup", data);
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
            await http.put<ICharacteristicGroup>(`api/CharacteristicGroup/${id}`, data);
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
            await http.delete(`api/CharacteristicGroup/${id}`);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const DeleteCharacteristicGroups = (ids: readonly number[]) => {
    return async () => {
        try {
            await http.delete(`api/CharacteristicGroup`, {
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
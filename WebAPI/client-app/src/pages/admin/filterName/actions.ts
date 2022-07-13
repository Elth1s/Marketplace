import { Dispatch } from "react"
import qs from "qs"

import http from "../../../http_comon"
import {
    IFilterName,
    IFilterNameInfo,
    FilterNameAction,
    FilterNameActionTypes,
    ISearchFilterNames,
} from "./types"
import { ServerError } from "../../../store/types"

export const GetByIdFilterName = (id: number) => {
    return async (dispatch: Dispatch<FilterNameAction>) => {
        try {
            let response = await http.get<IFilterName>(`api/FilterName/GetById/${id}`)

            dispatch({
                type: FilterNameActionTypes.GET_BY_ID_FILTER_NAME,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const SearchFilterNames = (page: number, rowsPerPage: number, name: string, isAscOrder: boolean, orderBy: string) => {
    return async (dispatch: Dispatch<FilterNameAction>) => {
        try {
            let response = await http.get<ISearchFilterNames>(`api/FilterName/Search`, {
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
                type: FilterNameActionTypes.SEARCH_FILTER_NAMES,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetFilterNames = () => {
    return async (dispatch: Dispatch<FilterNameAction>) => {
        try {
            let response = await http.get<Array<IFilterNameInfo>>(`api/FilterName/Get`)

            dispatch({
                type: FilterNameActionTypes.GET_FILTER_NAMES,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const CreateFilterName = (data: IFilterName) => {
    return async () => {
        try {
            await http.post("api/FilterName/Create", data);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const UpdateFilterName = (id: number, data: IFilterName) => {
    return async () => {
        try {
            await http.put<IFilterName>(`api/FilterName/Update/${id}`, data);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const DeleteFilterName = (id: number) => {
    return async () => {
        try {
            await http.delete(`api/FilterName/Delete/${id}`);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const DeleteFilterNames = (ids: readonly number[]) => {
    return async () => {
        try {
            await http.delete(`api/FilterName/Delete`, {
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
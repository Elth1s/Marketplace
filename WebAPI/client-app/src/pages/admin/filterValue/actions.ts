import { Dispatch } from "react"
import qs from "qs"

import http from "../../../http_comon"
import {
    IFilterValue,
    IFilterValueInfo,
    FilterValueAction,
    FilterValueActionTypes,
    ISearchFilterValues,
} from "./types"
import { ServerError } from "../../../store/types"

export const GetByIdFilterValue = (id: number) => {
    return async (dispatch: Dispatch<FilterValueAction>) => {
        try {
            let response = await http.get<IFilterValue>(`api/FilterValue/GetById/${id}`)
            dispatch({
                type: FilterValueActionTypes.GET_BY_ID_FILTER_VALUE,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const SearchFilterValues = (page: number, rowsPerPage: number, name: string, isAscOrder: boolean, orderBy: string) => {
    return async (dispatch: Dispatch<FilterValueAction>) => {
        try {
            let response = await http.get<ISearchFilterValues>(`api/FilterValue/Search`, {
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
                type: FilterValueActionTypes.SEARCH_FILTER_VALUE,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetFilterValues = () => {
    return async (dispatch: Dispatch<FilterValueAction>) => {
        try {
            let response = await http.get<Array<IFilterValueInfo>>(`api/FilterValue/Get`)

            dispatch({
                type: FilterValueActionTypes.GET_FILTER_VALUES,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const CreateFilterValue = (data: IFilterValue) => {
    return async () => {
        try {
            await http.post("api/FilterValue/Create", { value: data.value, min: data.min == "" ? null : data.min, max: data.max == "" ? null : data.max, filterNameId: data.filterNameId });
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const UpdateFilterValue = (id: number, data: IFilterValue) => {
    return async () => {
        try {
            await http.put<IFilterValue>(`api/FilterValue/Update/${id}`, { value: data.value, min: data.min == "" ? null : data.min, max: data.max == "" ? null : data.max, filterNameId: data.filterNameId });
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const DeleteFilterValue = (id: number) => {
    return async () => {
        try {
            await http.delete(`api/FilterValue/Delete/${id}`);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const DeleteFilterValues = (ids: readonly number[]) => {
    return async () => {
        try {
            await http.delete(`api/FilterValue/Delete`, {
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
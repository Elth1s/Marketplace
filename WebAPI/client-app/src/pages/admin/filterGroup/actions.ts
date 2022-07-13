import { Dispatch } from "react"
import qs from "qs"

import http from "../../../http_comon"
import {
    IFilterGroup,
    IFilterGroupInfo,
    FilterGroupAction,
    FilterGroupActionTypes,
    ISearchFilterGroups,
} from "./types"
import { ServerError } from "../../../store/types"

export const SearchFilterGroups = (page: number, rowsPerPage: number, name: string, isAscOrder: boolean, orderBy: string) => {
    return async (dispatch: Dispatch<FilterGroupAction>) => {
        try {
            let response = await http.get<ISearchFilterGroups>(`api/FilterGroup/Search`, {
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
                type: FilterGroupActionTypes.SEARCH_FILTER_GROUPS,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetByIdFilterGroup = (id: number) => {
    return async (dispatch: Dispatch<FilterGroupAction>) => {
        try {
            let response = await http.get<IFilterGroupInfo>(`api/FilterGroup/GetById/${id}`)

            dispatch({
                type: FilterGroupActionTypes.GET_BY_ID_FILTER_GROUP,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetFilterGroups = () => {
    return async (dispatch: Dispatch<FilterGroupAction>) => {
        try {
            let response = await http.get<Array<IFilterGroupInfo>>(`api/FilterGroup/Get`)

            dispatch({
                type: FilterGroupActionTypes.GET_FILTER_GROUPS,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const CreateFilterGroup = (data: IFilterGroup) => {
    return async () => {
        try {
            await http.post("api/FilterGroup/Create", data);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const UpdateFilterGroup = (id: number, data: IFilterGroup) => {
    return async () => {
        try {
            await http.put<IFilterGroup>(`api/FilterGroup/Update/${id}`, data);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const DeleteFilterGroup = (id: number) => {
    return async () => {
        try {
            await http.delete(`api/FilterGroup/Delete/${id}`);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const DeleteFilterGroups = (ids: readonly number[]) => {
    return async () => {
        try {
            await http.delete(`api/FilterGroup/Delete`, {
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
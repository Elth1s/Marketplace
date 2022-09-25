import { Dispatch } from "react"

import http from "../../../http_comon"
import {
    ICategory,
    ICategoryInfo,
    ICategoryForSelect,
    CategoryAction,
    CategoryActionTypes,
    ISearchCategories,
    ICategoryFilterValue,
} from "./types"
import { ServerError } from "../../../store/types"
import qs from "qs"

export const SearchCategories = (page: number, rowsPerPage: number, name: string, isAscOrder: boolean, orderBy: string) => {
    return async (dispatch: Dispatch<CategoryAction>) => {
        try {
            let response = await http.get<ISearchCategories>(`api/Category/Search`, {
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
                type: CategoryActionTypes.SEARCH_CATEGORIES,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetCategoryById = (id: number) => {
    return async (dispatch: Dispatch<CategoryAction>) => {
        try {
            let response = await http.get<ICategory>(`api/Category/GetById/${id}`)

            dispatch({
                type: CategoryActionTypes.GET_BY_ID_CATEGORY,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetCategories = () => {
    return async (dispatch: Dispatch<CategoryAction>) => {
        try {
            let response = await http.get<Array<ICategoryInfo>>(`api/Category/Get`)

            dispatch({
                type: CategoryActionTypes.GET_CATEGORIES,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetCategoryForSelect = () => {
    return async (dispatch: Dispatch<CategoryAction>) => {
        try {
            let response = await http.get<Array<ICategoryForSelect>>(`api/Category/GetForSelect`)

            dispatch({
                type: CategoryActionTypes.GET_CATEGORIES_FOR_SELECT,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetCategoryFiltersById = (id: number) => {
    return async (dispatch: Dispatch<CategoryAction>) => {
        try {
            let response = await http.get<Array<ICategoryFilterValue>>(`api/Category/GetFilters/${id}`)

            dispatch({
                type: CategoryActionTypes.GET_BY_ID_CATEGORY_FILTERS,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const CreateCategory = (data: ICategory) => {
    return async () => {
        try {
            await http.post("api/Category/Create", data);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const UpdateCategory = (id: number, data: ICategory) => {
    return async () => {
        try {
            await http.put(`api/Category/Update/${id}`, data);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const UpdateCategoryFilters = (id: number, data: Array<ICategoryFilterValue>) => {
    return async () => {
        try {
            await http.put(`api/Category/UpdateFilters/${id}`, data);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const DeleteCategory = (id: number) => {
    return async () => {
        try {
            await http.delete(`api/Category/Delete/${id}`);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const DeleteCategories = (ids: readonly number[]) => {
    return async () => {
        try {
            await http.delete(`api/Category/Delete`, {
                params: {
                    ids: ids,
                },
                paramsSerializer: params => {
                    return qs.stringify({ ...params })
                }
            })
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}
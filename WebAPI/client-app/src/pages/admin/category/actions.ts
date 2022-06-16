import { Dispatch } from "react"
import axios, { AxiosError } from "axios"

import http from "../../../http_comon"
import {
    ICategory,
    ICategoryInfo,
    ICategoryForSelect,
    CategoryAction,
    CategoryActionTypes,
} from "./types"
import { ServerError } from "../../../store/types"

export const GetByIdCategory = (id: string | null) => {
    return async (dispatch: Dispatch<CategoryAction>) => {
        try {
            let response = await http.get<ICategoryInfo>(`api/Category/GetById/${id}`)

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

export const GetCategory = () => {
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
            console.log("data", data);
            await http.put<ICategory>(`api/Category/Update/${id}`, data);
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
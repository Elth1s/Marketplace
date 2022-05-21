import { Dispatch } from "react"
import axios, { AxiosError } from "axios"

import http, { baseURL } from "../../http_comon"
import { ICategory, CategoryAction, CategoryActionTypes, ICreateCategory, ICategoryForSelect, CategoryServerError } from "./types"

export const GetByIdCategory = (id: string | null) => {
    return async (dispatch: Dispatch<CategoryAction>) => {
        try {
            let response = await http.get<ICategory>(`/api/Category/GetById/${id}`)
            
            dispatch({
                type: CategoryActionTypes.GET_BY_ID_CATEGORY,
                payload: response.data
            })

            return Promise.resolve();
        } catch (ex) {
            if (axios.isAxiosError(ex)) {
                const serverError = ex as AxiosError<CategoryServerError>;
                if (serverError && serverError.response) {
                    serverError.response.data.status = serverError.response.status;
                    return Promise.reject(serverError.response.data);
                }
            }
            return Promise.reject();
        }
    }
}

export const GetCategory = () => {
    return async (dispatch: Dispatch<CategoryAction>) => {
        try {
            let response = await http.get<Array<ICategory>>(`/api/Category/Get`)

            dispatch({
                type: CategoryActionTypes.GET_CATEGORIES,
                payload: response.data
            })

            return Promise.resolve();
        } catch (ex) {
            if (axios.isAxiosError(ex)) {
                const serverError = ex as AxiosError<CategoryServerError>;
                if (serverError && serverError.response) {
                    serverError.response.data.status = serverError.response.status;
                    return Promise.reject(serverError.response.data);
                }
            }
            return Promise.reject();
        }
    }
}

export const GetCategoryForSelect = () => {
    return async (dispatch: Dispatch<CategoryAction>) => {
        try {
            let response = await http.get<Array<ICategoryForSelect>>(`/api/Category/GetForSelect`)

            dispatch({
                type: CategoryActionTypes.GET_CATEGORIES_FOR_SELECT,
                payload: response.data
            })

            return Promise.resolve();
        } catch (ex) {
            if (axios.isAxiosError(ex)) {
                const serverError = ex as AxiosError<CategoryServerError>;
                if (serverError && serverError.response) {
                    serverError.response.data.status = serverError.response.status;
                    return Promise.reject(serverError.response.data);
                }
            }
            return Promise.reject();
        }
    }
}

export const CreateCategory = (data: ICreateCategory) => {
    return async () => {
        try {
            await http.post("api/Category/Create", data);
            return Promise.resolve();
        } catch (ex) {
            if (axios.isAxiosError(ex)) {
                const serverError = ex as AxiosError<CategoryServerError>;
                if (serverError && serverError.response) {
                    serverError.response.data.status = serverError.response.status;
                    return Promise.reject(serverError.response.data);
                }
            }
            return Promise.reject();
        }
    }
}

export const UpdateCategory = (data: ICategory) => {
    return async () => {
        try {
            await http.put<ICategory>(`api/Category/Update/${data.id}`, data);
            return Promise.resolve();
        } catch (ex) {
            if (axios.isAxiosError(ex)) {
                const serverError = ex as AxiosError<CategoryServerError>;
                if (serverError && serverError.response) {
                    serverError.response.data.status = serverError.response.status;
                    return Promise.reject(serverError.response.data);
                }
            }
            return Promise.reject();
        }
    }
}

export const DeleteCategory = (id: number) => {
    return async () => {
        try {
            await http.delete(`api/Category/Delete/${id}`);
            return Promise.resolve();
        } catch (ex) {
            if (axios.isAxiosError(ex)) {
                const serverError = ex as AxiosError<CategoryServerError>;
                if (serverError && serverError.response) {
                    serverError.response.data.status = serverError.response.status;
                    return Promise.reject(serverError.response.data);
                }
            }
            return Promise.reject();
        }
    }
}
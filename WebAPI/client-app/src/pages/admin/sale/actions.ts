import { Dispatch } from "react"

import http from "../../../http_comon"
import {
    ISale,
    ISaleInfo,
    SaleAction,
    SaleActionTypes,
    ISearchSales,
} from "./types"
import { ServerError } from "../../../store/types"
import qs from "qs"

export const SearchSales = (page: number, rowsPerPage: number, name: string, isAscOrder: boolean, orderBy: string) => {
    return async (dispatch: Dispatch<SaleAction>) => {
        try {
            let response = await http.get<ISearchSales>(`api/Sale/Search`, {
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
                type: SaleActionTypes.SEARCH_SALES,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetSaleById = (id: number) => {
    return async (dispatch: Dispatch<SaleAction>) => {
        try {
            let response = await http.get<ISale>(`api/Sale/GetById/${id}`)

            dispatch({
                type: SaleActionTypes.GET_BY_ID_SALE,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetSales = () => {
    return async (dispatch: Dispatch<SaleAction>) => {
        try {
            let response = await http.get<Array<ISaleInfo>>(`api/Sale/GetAll`)

            dispatch({
                type: SaleActionTypes.GET_SALES,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const CreateSale = (data: ISale) => {
    return async () => {
        try {
            data.dateStart = new Date((data.dateStart as Date).getTime() - ((data.dateStart as Date).getTimezoneOffset() * 60000)).toISOString();
            data.dateEnd = new Date((data.dateEnd as Date).getTime() - ((data.dateEnd as Date).getTimezoneOffset() * 60000)).toISOString();
            await http.post("api/Sale/Create", data);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const UpdateSale = (id: number, data: ISale) => {
    return async () => {
        try {
            await http.put(`api/Sale/Update/${id}`, data);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const DeleteSale = (id: number) => {
    return async () => {
        try {
            await http.delete(`api/Sale/Delete/${id}`);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const DeleteSales = (ids: readonly number[]) => {
    return async () => {
        try {
            await http.delete(`api/Sale/Delete`, {
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
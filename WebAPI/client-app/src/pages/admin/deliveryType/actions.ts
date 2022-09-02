import { Dispatch } from "react"

import http from "../../../http_comon"
import {
    IDeliveryType,
    IDeliveryTypeInfo,
    IDeliveryTypeForSelect,
    DeliveryTypeAction,
    DeliveryTypeActionTypes,
    ISearchDeliveryTypes,
} from "./types"
import { ServerError } from "../../../store/types"
import qs from "qs"

export const SearchDeliveryTypes = (page: number, rowsPerPage: number, name: string, isAscOrder: boolean, orderBy: string) => {
    return async (dispatch: Dispatch<DeliveryTypeAction>) => {
        try {
            let response = await http.get<ISearchDeliveryTypes>(`api/DeliveryType/Search`, {
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
                type: DeliveryTypeActionTypes.SEARCH_DELIVERY_TYPES,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetDeliveryTypeById = (id: number) => {
    return async (dispatch: Dispatch<DeliveryTypeAction>) => {
        try {
            let response = await http.get<IDeliveryType>(`api/DeliveryType/GetById/${id}`)

            dispatch({
                type: DeliveryTypeActionTypes.GET_BY_ID_DELIVERY_TYPES,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetDeliveryTypes = () => {
    return async (dispatch: Dispatch<DeliveryTypeAction>) => {
        try {
            let response = await http.get<Array<IDeliveryTypeInfo>>(`api/DeliveryType/Get`)

            dispatch({
                type: DeliveryTypeActionTypes.GET_DELIVERY_TYPES,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetDeliveryTypeForSelect = () => {
    return async (dispatch: Dispatch<DeliveryTypeAction>) => {
        try {
            let response = await http.get<Array<IDeliveryTypeForSelect>>(`api/DeliveryType/GetForSelect`)

            dispatch({
                type: DeliveryTypeActionTypes.GET_DELIVERY_TYPES_FOR_SELECT,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const CreateDeliveryType = (data: IDeliveryType) => {
    return async () => {
        try {
            await http.post("api/DeliveryType/Create", data);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const UpdateDeliveryType = (id: number, data: IDeliveryType) => {
    return async () => {
        try {
            await http.put<IDeliveryType>(`api/DeliveryType/Update/${id}`, data);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const DeleteDeliveryType = (id: number) => {
    return async () => {
        try {
            await http.delete(`api/DeliveryType/Delete/${id}`);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const DeleteDeliveryTypes = (ids: readonly number[]) => {
    return async () => {
        try {
            await http.delete(`api/DeliveryType/Delete`, {
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
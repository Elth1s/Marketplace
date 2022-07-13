import { Dispatch } from "react"

import http from "../../../http_comon"
import {
    ICity,
    ICityInfo,
    CityAction,
    CityActionTypes,
    ISearchCities,
} from "./types"
import { ServerError } from "../../../store/types"
import qs from "qs"

export const SearchCities = (page: number, rowsPerPage: number, name: string, isAscOrder: boolean, orderBy: string) => {
    return async (dispatch: Dispatch<CityAction>) => {
        try {
            let response = await http.get<ISearchCities>(`api/City/Search`, {
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
                type: CityActionTypes.SEARCH_CITIES,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetCityById = (id: number) => {
    return async (dispatch: Dispatch<CityAction>) => {
        try {
            let response = await http.get<ICity>(`api/City/GetById/${id}`)

            dispatch({
                type: CityActionTypes.GET_BY_ID_CITY,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetCities = () => {
    return async (dispatch: Dispatch<CityAction>) => {
        try {
            let response = await http.get<Array<ICityInfo>>(`api/City/Get`)

            dispatch({
                type: CityActionTypes.GET_CITIES,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const CreateCity = (data: ICity) => {
    return async () => {
        try {
            await http.post("api/City/Create", data);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const UpdateCity = (id: number, data: ICity) => {
    return async () => {
        try {
            await http.put<ICity>(`api/City/Update/${id}`, data);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const DeleteCity = (id: number) => {
    return async () => {
        try {
            await http.delete(`api/City/Delete/${id}`);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const DeleteCities = (ids: readonly number[]) => {
    return async () => {
        try {
            await http.delete(`api/City/Delete`, {
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
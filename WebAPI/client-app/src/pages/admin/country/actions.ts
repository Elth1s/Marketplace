import { Dispatch } from "react"
import qs from "qs"

import http from "../../../http_comon"
import {
    ICountry,
    ICountryInfo,
    CountryAction,
    CountryActionTypes,
    ISearchCountries,
} from "./types"
import { ServerError } from "../../../store/types"

export const SearchCountries = (page: number, rowsPerPage: number, name: string, isAscOrder: boolean, orderBy: string) => {
    return async (dispatch: Dispatch<CountryAction>) => {
        try {
            let response = await http.get<ISearchCountries>(`api/Country/SearchCountries`, {
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
                type: CountryActionTypes.SEARCH_COUNTRIES,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetCountries = () => {
    return async (dispatch: Dispatch<CountryAction>) => {
        try {
            let response = await http.get<Array<ICountryInfo>>(`api/Country/GetCountries`)

            dispatch({
                type: CountryActionTypes.GET_COUNTRIES,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetByIdCountry = (id: number) => {
    return async (dispatch: Dispatch<CountryAction>) => {
        try {
            let response = await http.get<ICountryInfo>(`api/Country/GetCountryById/${id}`)

            dispatch({
                type: CountryActionTypes.GET_BY_ID_COUNTRY,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const CreateCountry = (data: ICountry) => {
    return async () => {
        try {
            await http.post("api/Country/CreateCountry", data);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const UpdateCountry = (id: number, data: ICountry) => {
    return async () => {
        try {
            await http.put<ICountry>(`api/Country/UpdateCountry/${id}`, data);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const DeleteCountry = (id: number) => {
    return async () => {
        try {
            await http.delete(`api/Country/DeleteCountry/${id}`);
            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const DeleteCountries = (ids: readonly number[]) => {
    return async () => {
        try {
            await http.delete(`api/Country/DeleteCountries`, {
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
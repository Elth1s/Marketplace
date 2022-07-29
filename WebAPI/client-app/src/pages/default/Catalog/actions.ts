import qs from "qs"
import { Dispatch } from "react"

import http from "../../../http_comon"
import { ServerError } from "../../../store/types"
import { CatalogAction, CatalogActionTypes, ICatalogItem, ICatalogWithProducts, IFilterName, IFullCatalogItem, IProductItem } from "./types"


export const GetCatalog = () => {
    return async (dispatch: Dispatch<CatalogAction>) => {
        try {
            let response = await http.get<Array<ICatalogItem>>(`api/Category/GetCatalog`);

            dispatch({
                type: CatalogActionTypes.GET_CATALOG,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetFullCatalog = () => {
    return async (dispatch: Dispatch<CatalogAction>) => {
        try {
            let response = await http.get<Array<IFullCatalogItem>>(`api/Category/GetFullCatalog`);

            dispatch({
                type: CatalogActionTypes.GET_FULL_CATALOG,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetFiltersByCategory = (urlSlug: string) => {
    return async (dispatch: Dispatch<CatalogAction>) => {
        try {
            let response = await http.get<Array<IFilterName>>(`api/Category/GetFiltersByCategory`, {
                params: {
                    urlSlug: urlSlug,
                },
                paramsSerializer: params => {
                    return qs.stringify({ ...params })
                }
            });

            dispatch({
                type: CatalogActionTypes.GET_FILTERS_BY_CATEGORY,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetCatalogWithProducts = (urlSlug: string, page: number, rowsPerPage: number, filters: Array<number>) => {
    return async (dispatch: Dispatch<CatalogAction>) => {
        try {
            let response = await http.get<ICatalogWithProducts>(`api/Category/GetCatalogWithProducts`, {
                params: {
                    urlSlug: urlSlug,
                    page: page,
                    rowsPerPage: rowsPerPage,
                    filters: filters
                },
                paramsSerializer: params => {
                    return qs.stringify({ ...params })
                }
            });

            dispatch({
                type: CatalogActionTypes.GET_CATALOG_WITH_PRODUCTS,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetMoreProducts = (urlSlug: string, page: number) => {
    return async (dispatch: Dispatch<CatalogAction>) => {
        try {
            let response = await http.get<Array<IProductItem>>(`api/Category/GetMoreProducts`, {
                params: {
                    urlSlug: urlSlug,
                    page: page,
                    rowsPerPage: 0,
                    filters: []
                },
                paramsSerializer: params => {
                    return qs.stringify({ ...params })
                }
            });

            dispatch({
                type: CatalogActionTypes.GET_MORE_PRODUCTS,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetParents = (urlSlug: string) => {
    return async (dispatch: Dispatch<CatalogAction>) => {
        try {
            let response = await http.get<Array<ICatalogItem>>(`api/Category/GetParents`, {
                params: {
                    urlSlug: urlSlug,
                },
                paramsSerializer: params => {
                    return qs.stringify({ ...params })
                }
            });

            dispatch({
                type: CatalogActionTypes.GET_PARENTS,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}
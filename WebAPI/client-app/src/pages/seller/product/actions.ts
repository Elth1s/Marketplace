import { Dispatch } from "react"
import qs from "qs"

import http from "../../../http_comon"
import {
    IProductInfo,
    ProductAction,
    ProductActionTypes,
    ISearchProducts,
    IProductStatus,
    ICategory,
    IProductImage,
    IFilterGroupSeller,
    IProductRequest,
    ICharacteristicGroupSeller,
} from "./types"
import { ServerError } from "../../../store/types"

export const SearchProducts = (page: number, rowsPerPage: number, name: string, isAscOrder: boolean, orderBy: string, isSeller: boolean) => {
    return async (dispatch: Dispatch<ProductAction>) => {
        try {
            let response = await http.get<ISearchProducts>(`api/Product/AdminSellerSearch`, {
                params: {
                    page: page,
                    rowsPerPage: rowsPerPage,
                    name: name,
                    isAscOrder: isAscOrder,
                    orderBy: orderBy,
                    isSeller: isSeller
                },
                paramsSerializer: params => {
                    return qs.stringify({ ...params })
                }
            })

            dispatch({
                type: ProductActionTypes.SEARCH_PRODUCTS,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetProductStatusesSeller = () => {
    return async (dispatch: Dispatch<ProductAction>) => {
        try {
            let response = await http.get<Array<IProductStatus>>(`api/ProductStatus/Get`)

            dispatch({
                type: ProductActionTypes.GET_PRODUCT_STATUSES,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetCategoriesWithoutChildren = () => {
    return async (dispatch: Dispatch<ProductAction>) => {
        try {
            let response = await http.get<Array<ICategory>>(`api/Category/GetWithoutChildren`)

            dispatch({
                type: ProductActionTypes.GET_CATEGORIES_WITHOUT_CHILDREN,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetFiltersByCategoryId = (id: number) => {
    return async (dispatch: Dispatch<ProductAction>) => {
        try {
            let response = await http.get<Array<IFilterGroupSeller>>(`api/Category/GetFiltersByCategory/${id}`)

            dispatch({
                type: ProductActionTypes.GET_FILTERS_BY_CATEGORY_ID,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetCharacteristicsByUser = () => {
    return async (dispatch: Dispatch<ProductAction>) => {
        try {
            let response = await http.get<Array<ICharacteristicGroupSeller>>(`api/CharacteristicValue/GetCharacteristicsByUser`)

            dispatch({
                type: ProductActionTypes.GET_CHARACTERISTICS_BY_USER,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetProducts = () => {
    return async (dispatch: Dispatch<ProductAction>) => {
        try {
            let response = await http.get<Array<IProductInfo>>(`api/Product/Get`)

            dispatch({
                type: ProductActionTypes.GET_PRODUCTS,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const CreateProductImage = (base64: string) => {
    return async (dispatch: Dispatch<ProductAction>) => {
        try {
            let response = await http.post<IProductImage>(`api/ProductImage/Create`, base64)

            return Promise.resolve(response.data);
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const CreateProduct = (values: IProductRequest) => {
    return async (dispatch: Dispatch<ProductAction>) => {
        try {
            let response = await http.post(`api/Product/Create`, values)

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const DeleteProducts = (ids: readonly number[]) => {
    return async () => {
        try {
            await http.delete(`api/Product/Delete`, {
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
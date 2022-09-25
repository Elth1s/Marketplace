import qs from "qs"
import { Dispatch } from "react"

import http from "../../../http_comon"
import { ServerError } from "../../../store/types"
import { CatalogAction, CatalogActionTypes, ICatalogItem, ICatalogWithProducts, IFilterName, IFullCatalogItem, IProductItem, IProductResponse, IUserProductItem } from "./types"


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

export const GetFiltersByCategoryIdForUser = (id: number) => {
    return async (dispatch: Dispatch<CatalogAction>) => {
        try {
            let response = await http.get<Array<IFilterName>>(`api/Category/GetFiltersByCategoryId`, {
                params: {
                    id: id,
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

export const ResetCatalogFilters = () => {
    return async (dispatch: Dispatch<CatalogAction>) => {
        dispatch({
            type: CatalogActionTypes.RESET_FILTERS
        })

        return Promise.resolve();
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

export const SearchProductsForUser = (productName: string, page: number, rowsPerPage: number, categories: Array<number>, filters: Array<number>) => {
    return async (dispatch: Dispatch<CatalogAction>) => {
        try {
            let response = await http.get<IProductResponse>(`api/Product/Search`, {
                params: {
                    productName: productName,
                    shopId: null,
                    page: page,
                    rowsPerPage: rowsPerPage,
                    categories: categories,
                    filters: filters
                },
                paramsSerializer: params => {
                    return qs.stringify({ ...params })
                }
            });

            dispatch({
                type: CatalogActionTypes.SEARCH_PRODUCTS,
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
                    rowsPerPage: 5,
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

export const UpdateSearch = (searchField: string) => {
    return async (dispatch: Dispatch<CatalogAction>) => {
        dispatch({
            type: CatalogActionTypes.UPDATE_SEARCH,
            payload: searchField
        })
    }
}

export const GetCategoriesForSearch = (productName: string) => {
    return async (dispatch: Dispatch<CatalogAction>) => {
        try {
            let response = await http.get<Array<IFullCatalogItem>>(`api/Category/GetCategoriesByProducts`, {
                params: {
                    productName: productName,
                    shopId: null
                },
                paramsSerializer: params => {
                    return qs.stringify({ ...params })
                }
            });

            dispatch({
                type: CatalogActionTypes.GET_CATEGORIES_FOR_SEARCH,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const SearchSellerProducts = (shopId: string, page: number, rowsPerPage: number, categories: Array<number>, filters: Array<number>) => {
    return async (dispatch: Dispatch<CatalogAction>) => {
        try {
            let response = await http.get<IProductResponse>(`api/Product/Search`, {
                params: {
                    productName: "",
                    shopId: shopId,
                    page: page,
                    rowsPerPage: rowsPerPage,
                    categories: categories,
                    filters: filters
                },
                paramsSerializer: params => {
                    return qs.stringify({ ...params })
                }
            });

            dispatch({
                type: CatalogActionTypes.SEARCH_PRODUCTS,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetCategoriesByShopId = (shopId: string) => {
    return async (dispatch: Dispatch<CatalogAction>) => {
        try {
            let response = await http.get<Array<IFullCatalogItem>>(`api/Category/GetCategoriesByProducts`, {
                params: {
                    productName: "",
                    shopId: shopId
                },
                paramsSerializer: params => {
                    return qs.stringify({ ...params })
                }
            });

            dispatch({
                type: CatalogActionTypes.GET_CATEGORIES_FOR_SEARCH,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetNovelties = (page: number, rowsPerPage: number, categories: Array<number>, filters: Array<number>) => {
    return async (dispatch: Dispatch<CatalogAction>) => {
        try {
            let response = await http.get<IProductResponse>(`api/Product/GetNovelties`, {
                params: {
                    page: page,
                    rowsPerPage: rowsPerPage,
                    categories: categories,
                    filters: filters
                },
                paramsSerializer: params => {
                    return qs.stringify({ ...params })
                }
            });

            dispatch({
                type: CatalogActionTypes.GET_NOVELTIES,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const ChangeIsSelectedProducts = (productUrlSlug: string) => {
    return async (dispatch: Dispatch<CatalogAction>) => {
        try {
            let response = await http.put(`api/Product/ChangeSelectProduct/${productUrlSlug}`);

            dispatch({
                type: CatalogActionTypes.CHANGE_IS_SELECTED_PRODUCTS,
                payload: productUrlSlug
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const ChangeIsSelectedUserProducts = (productUrlSlug: string) => {
    return async (dispatch: Dispatch<CatalogAction>) => {
        try {
            let response = await http.put(`api/Product/ChangeSelectProduct/${productUrlSlug}`);

            dispatch({
                type: CatalogActionTypes.CHANGE_IS_SELECTED_USER_PRODUCTS,
                payload: productUrlSlug
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const ChangeIsInCartUserProducts = (productUrlSlug: string) => {
    return async (dispatch: Dispatch<CatalogAction>) => {
        try {
            dispatch({
                type: CatalogActionTypes.CHANGE_IS_IN_CART_USER_PRODUCTS,
                payload: productUrlSlug
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetSimilarProducts = (urlSlug: string) => {
    return async (dispatch: Dispatch<CatalogAction>) => {
        try {
            let response = await http.get<Array<IProductItem>>(`api/Product/GetSimilarProducts`, {
                params: {
                    urlSlug: urlSlug,
                },
                paramsSerializer: params => {
                    return qs.stringify({ ...params })
                }
            });

            dispatch({
                type: CatalogActionTypes.GET_SIMILAR_PRODUCTS,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetSelectedProducts = () => {
    return async (dispatch: Dispatch<CatalogAction>) => {
        try {
            let response = await http.get<Array<IUserProductItem>>(`api/Product/GetSelectedProducts`);

            dispatch({
                type: CatalogActionTypes.GET_SELECTED_PRODUCTS,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetReviewedProducts = () => {
    return async (dispatch: Dispatch<CatalogAction>) => {
        try {
            let response = await http.get<Array<IUserProductItem>>(`api/Product/GetReviewedProducts`);

            dispatch({
                type: CatalogActionTypes.GET_REVIEWED_PRODUCTS,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const GetCategoriesBySaleId = (saleId: string) => {
    return async (dispatch: Dispatch<CatalogAction>) => {
        try {
            let response = await http.get<Array<IFullCatalogItem>>(`api/Category/GetCategoriesBySale/${saleId}`);

            dispatch({
                type: CatalogActionTypes.GET_SALE_CATEGORIES,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}

export const SaleProducts = (saleId: string, page: number, rowsPerPage: number, categories: Array<number>, filters: Array<number>) => {
    return async (dispatch: Dispatch<CatalogAction>) => {
        try {
            let response = await http.get<IProductResponse>(`api/Product/GetProductsBySale`, {
                params: {
                    saleId: saleId,
                    page: page,
                    rowsPerPage: rowsPerPage,
                    categories: categories,
                    filters: filters
                },
                paramsSerializer: params => {
                    return qs.stringify({ ...params })
                }
            });

            dispatch({
                type: CatalogActionTypes.GET_SALE_PRODUCTS,
                payload: response.data
            })

            return Promise.resolve();
        }
        catch (error) {
            return Promise.reject(error as ServerError)
        }
    }
}
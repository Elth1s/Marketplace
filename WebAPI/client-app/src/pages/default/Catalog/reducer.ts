import { CatalogAction, CatalogActionTypes, CatalogState } from "./types";

const initialState: CatalogState = {
    parents: [],
    name: "",
    catalogItems: [],
    fullCatalogItems: [],
    products: [],
    countProducts: 0,
    filterNames: [],
    searchField: "",
    searchCatalog: [],
    userProducts: [],
    min: 0,
    max: 0
}

export const catalogReducer = (state = initialState, action: CatalogAction): CatalogState => {
    switch (action.type) {
        case CatalogActionTypes.GET_CATALOG:
            return {
                ...state,
                catalogItems: action.payload,
            }
        case CatalogActionTypes.GET_FULL_CATALOG:
            return {
                ...state,
                fullCatalogItems: action.payload,
            }
        case CatalogActionTypes.GET_FILTERS_BY_CATEGORY:
            return {
                ...state,
                filterNames: action.payload,
            }
        case CatalogActionTypes.RESET_FILTERS:
            return {
                ...state,
                filterNames: [],
            }
        case CatalogActionTypes.GET_CATALOG_WITH_PRODUCTS:
            return {
                ...state,
                name: action.payload.name,
                catalogItems: action.payload.catalogItems,
                products: action.payload.products,
                countProducts: action.payload.countProducts,
                min: action.payload.min,
                max: action.payload.max
            }
        case CatalogActionTypes.GET_MORE_PRODUCTS:
            return {
                ...state,
                products: [...state.products, ...action.payload]
            }
        case CatalogActionTypes.GET_PARENTS:
            return {
                ...state,
                parents: action.payload,
            }
        case CatalogActionTypes.UPDATE_SEARCH:
            return {
                ...state,
                searchField: action.payload,
            }
        case CatalogActionTypes.SEARCH_PRODUCTS:
            return {
                ...state,
                countProducts: action.payload.count,
                products: action.payload.values
            }
        case CatalogActionTypes.GET_CATEGORIES_FOR_SEARCH:
            return {
                ...state,
                searchCatalog: action.payload,
                filterNames: []
            }
        case CatalogActionTypes.GET_NOVELTIES:
            return {
                ...state,
                countProducts: action.payload.count,
                products: action.payload.values,
                filterNames: []
            }
        case CatalogActionTypes.CHANGE_IS_SELECTED_PRODUCTS:
            let product = state.products.find(value => value.urlSlug === action.payload);
            if (product) {
                let index = state.products.indexOf(product);
                let tempProducts = state.products.slice();
                tempProducts[index].isSelected = !tempProducts[index].isSelected;
                return {
                    ...state,
                    products: tempProducts
                }
            }
            else
                return state;
        case CatalogActionTypes.CHANGE_IS_SELECTED_USER_PRODUCTS:
            let userproduct = state.userProducts.find(value => value.urlSlug === action.payload);
            if (userproduct) {
                let index = state.userProducts.indexOf(userproduct);
                let tempProducts = state.userProducts.slice();
                tempProducts[index].isSelected = !tempProducts[index].isSelected;
                return {
                    ...state,
                    userProducts: tempProducts
                }
            }
            else
                return state;
        case CatalogActionTypes.CHANGE_IS_IN_CART_USER_PRODUCTS:
            let cartproduct = state.userProducts.find(value => value.urlSlug === action.payload);
            if (cartproduct) {
                let index = state.userProducts.indexOf(cartproduct);
                let tempProducts = state.userProducts.slice();
                tempProducts[index].isInBasket = !tempProducts[index].isInBasket;
                return {
                    ...state,
                    userProducts: tempProducts
                }
            }
            else
                return state;
        case CatalogActionTypes.GET_SIMILAR_PRODUCTS:
            return {
                ...state,
                products: action.payload,
            }
        case CatalogActionTypes.GET_SELECTED_PRODUCTS:
            return {
                ...state,
                userProducts: action.payload,
            }
        case CatalogActionTypes.GET_REVIEWED_PRODUCTS:
            return {
                ...state,
                userProducts: action.payload,
            }
        case CatalogActionTypes.GET_SALE_PRODUCTS:
            return {
                ...state,
                countProducts: action.payload.count,
                products: action.payload.values
            }
        case CatalogActionTypes.GET_SALE_CATEGORIES:
            return {
                ...state,
                searchCatalog: action.payload,
                filterNames: []
            }
        default:
            return state;
    }
}
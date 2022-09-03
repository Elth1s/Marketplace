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
    searchCatalog: []
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
        case CatalogActionTypes.GET_CATALOG_WITH_PRODUCTS:
            return {
                ...state,
                name: action.payload.name,
                catalogItems: action.payload.catalogItems,
                products: action.payload.products,
                countProducts: action.payload.countProducts,
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
            }
        default:
            return state;
    }
}
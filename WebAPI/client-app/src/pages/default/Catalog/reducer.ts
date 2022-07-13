import { CatalogAction, CatalogActionTypes, CatalogState } from "./types";

const initialState: CatalogState = {
    parents: [],
    name: "Catalog",
    catalogItems: [],
    products: [],
    count: 0,
    filterNames: []
}

export const catalogReducer = (state = initialState, action: CatalogAction): CatalogState => {
    switch (action.type) {
        case CatalogActionTypes.GET_CATALOG:
            return {
                ...state,
                catalogItems: action.payload,
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
                count: action.payload.count,
            }
        case CatalogActionTypes.GET_PARENTS:
            return {
                ...state,
                parents: action.payload,
            }
        default:
            return state;
    }
}
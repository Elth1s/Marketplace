import { CategoryAction, CategoryActionTypes, CategoryState } from "./types";

const initialState: CategoryState = {
    selectedCategory: {
        id: 0,
        englishName: "",
        ukrainianName: "",
        urlSlug: "",
        image: "",
        lightIcon: "",
        darkIcon: "",
        activeIcon: "",
        parentId: 0
    },
    selectedCategoryFilters: [],
    categories: [],
    categoriesForSelect: [],
    count: 0,
}

export const categoryReducer = (state = initialState, action: CategoryAction): CategoryState => {
    switch (action.type) {
        case CategoryActionTypes.GET_CATEGORIES:
            return {
                ...state,
                categories: action.payload,
            }
        case CategoryActionTypes.SEARCH_CATEGORIES:
            return {
                ...state,
                categories: action.payload.values,
                count: action.payload.count,
            }
        case CategoryActionTypes.GET_CATEGORIES_FOR_SELECT:
            return {
                ...state,
                categoriesForSelect: action.payload,
            }
        case CategoryActionTypes.GET_BY_ID_CATEGORY:
            return {
                ...state,
                selectedCategory: action.payload,
            }
        case CategoryActionTypes.GET_BY_ID_CATEGORY_FILTERS:
            return {
                ...state,
                selectedCategoryFilters: action.payload,
            }
        default:
            return state;
    }
}
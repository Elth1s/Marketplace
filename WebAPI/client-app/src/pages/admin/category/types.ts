export enum CategoryActionTypes {
    GET_CATEGORIES = "GET_CATEGORIES",
    SEARCH_CATEGORIES = "SEARCH_CATEGORIES",
    GET_CATEGORIES_FOR_SELECT = "GET_CATEGORIES_FOR_SELECT",
    GET_BY_ID_CATEGORY = "GET_BY_ID_CATEGORY",
    CREATE_CATEGOTY = "CREATE_CATEGOTY",
    UPDATE_CATEGORY = "UPDATE_CATEGORY",
}

export interface ICategory {
    id: number,
    englishName: string,
    ukrainianName: string,
    urlSlug: string,
    image: string,
    icon: string,
    parentId: number | null
}

export interface ICategoryInfo {
    id: number,
    name: string,
    urlSlug: string,
    image: string,
    icon: string,
    parentName: string
}

export interface ICategoryForSelect {
    id: number
    name: string,
}

export interface ISearchCategories {
    count: number,
    values: Array<ICategoryInfo>
}


export interface CategoryState {
    selectedCategory: ICategory,
    categories: Array<ICategoryInfo>,
    categoriesForSelect: Array<ICategoryForSelect>,
    count: number
}



export interface GetCategoriesAction {
    type: CategoryActionTypes.GET_CATEGORIES,
    payload: Array<ICategoryInfo>
}

export interface SearchCategoriesAction {
    type: CategoryActionTypes.SEARCH_CATEGORIES,
    payload: ISearchCategories
}


export interface GetCategoriesForSelectAction {
    type: CategoryActionTypes.GET_CATEGORIES_FOR_SELECT,
    payload: Array<ICategoryForSelect>
}

export interface GetByIdCategoryAction {
    type: CategoryActionTypes.GET_BY_ID_CATEGORY,
    payload: ICategory
}

export interface CreateCategoryAction {
    type: CategoryActionTypes.CREATE_CATEGOTY,
    payload: ICategory
}

export interface UpdateCategoryAction {
    type: CategoryActionTypes.UPDATE_CATEGORY,
    payload: ICategory
}


export type CategoryAction = GetCategoriesAction |
    GetCategoriesForSelectAction |
    SearchCategoriesAction |
    GetByIdCategoryAction |
    CreateCategoryAction |
    UpdateCategoryAction;
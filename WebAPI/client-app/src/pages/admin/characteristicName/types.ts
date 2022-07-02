export enum CharacteristicNameActionTypes {
    GET_CHARACTERISTIC_NAMES = "GET_CHARACTERISTIC_NAMES",
    GET_BY_ID_CHARACTERISTIC_NAME = "GET_BY_ID_CHARACTERISTIC_NAME",
    CREATE_CHARACTERISTIC_NAME = "CREATE_CHARACTERISTIC_NAME",
    UPDATE_CHARACTERISTIC_NAME = "UPDATE_CHARACTERISTIC_NAME",
    SEARCH_CHARACTERISTIC_NAMES = "SEARCH_CHARACTERISTIC_NAMES",
}

export interface ICharacteristicName {
    name: string,
    characteristicGroupId: number,
    unitId: number | null,
}

export interface ICharacteristicNameInfo {
    id: number,
    name: string,
    characteristicGroupName: string,
    unitMeasure: string,
}

export interface ISearchCharacteristicNames {
    count: number,
    values: Array<ICharacteristicNameInfo>
}

export interface CharacteristicNameState {
    selectedCharacteristicName: ICharacteristicName,
    count: number,
    characteristicNames: Array<ICharacteristicNameInfo>
}


export interface GetCharacteristicNamesAction {
    type: CharacteristicNameActionTypes.GET_CHARACTERISTIC_NAMES,
    payload: Array<ICharacteristicNameInfo>
}

export interface SearchCharacteristicNamesAction {
    type: CharacteristicNameActionTypes.SEARCH_CHARACTERISTIC_NAMES,
    payload: ISearchCharacteristicNames
}

export interface GetByIdCharacteristicNameAction {
    type: CharacteristicNameActionTypes.GET_BY_ID_CHARACTERISTIC_NAME,
    payload: ICharacteristicName
}

export interface CreateCharacteristicNameAction {
    type: CharacteristicNameActionTypes.CREATE_CHARACTERISTIC_NAME,
    payload: ICharacteristicName
}

export interface UpdateCharacteristicNameAction {
    type: CharacteristicNameActionTypes.UPDATE_CHARACTERISTIC_NAME,
    payload: ICharacteristicName
}


export type CharacteristicNameAction = GetCharacteristicNamesAction |
    SearchCharacteristicNamesAction |
    GetByIdCharacteristicNameAction |
    CreateCharacteristicNameAction |
    UpdateCharacteristicNameAction;
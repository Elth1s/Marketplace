export enum CharacteristicValueActionTypes {
    GET_CHARACTERISTIC_VALUES = "GET_CHARACTERISTIC_VALUES",
    GET_BY_ID_CHARACTERISTIC_VALUE = "GET_BY_ID_CHARACTERISTIC_VALUE",
    CREATE_CHARACTERISTIC_VALUE = "CREATE_CHARACTERISTIC_VALUE",
    UPDATE_CHARACTERISTIC_VALUE = "UPDATE_CHARACTERISTIC_VALUE",
    SEARCH_CHARACTERISTIC_VALUE = "SEARCH_CHARACTERISTIC_VALUE",
}

export interface ICharacteristicValue {
    value: string,
    characteristicNameId: number,
}

export interface ICharacteristicValueInfo {
    id: number,
    value: string,
    characteristicName: string,
}

export interface ISearchCharacteristicValues {
    count: number,
    values: Array<ICharacteristicValueInfo>
}

export interface CharacteristicValueState {
    selectedCharacteristicValue: ICharacteristicValue,
    count: number,
    characteristicValues: Array<ICharacteristicValueInfo>
}


export interface GetCharacteristicValuesAction {
    type: CharacteristicValueActionTypes.GET_CHARACTERISTIC_VALUES,
    payload: Array<ICharacteristicValueInfo>
}

export interface SearchCharacteristicValuesAction {
    type: CharacteristicValueActionTypes.SEARCH_CHARACTERISTIC_VALUE,
    payload: ISearchCharacteristicValues
}

export interface GetByIdCharacteristicValueAction {
    type: CharacteristicValueActionTypes.GET_BY_ID_CHARACTERISTIC_VALUE,
    payload: ICharacteristicValue
}

export interface CreateCharacteristicValueAction {
    type: CharacteristicValueActionTypes.CREATE_CHARACTERISTIC_VALUE,
    payload: ICharacteristicValue
}

export interface UpdateCharacteristicValueAction {
    type: CharacteristicValueActionTypes.UPDATE_CHARACTERISTIC_VALUE,
    payload: ICharacteristicValue
}


export type CharacteristicValueAction = GetCharacteristicValuesAction |
    SearchCharacteristicValuesAction |
    GetByIdCharacteristicValueAction |
    CreateCharacteristicValueAction |
    UpdateCharacteristicValueAction;
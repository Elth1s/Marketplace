import { FieldInputProps } from "formik/dist/types";

export interface ISelect {
    label: string,
    items: Array<IItem>,
    touched?: boolean | undefined,
    error?: string | undefined,
    getFieldProps: FieldInputProps<any>,
}

export interface IItem {
    id: number,
    name: string
}
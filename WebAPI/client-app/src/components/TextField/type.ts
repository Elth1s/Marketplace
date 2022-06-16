import { FieldInputProps } from "formik/dist/types";

export interface ITextField {
    label: string,
    touched?: boolean | undefined,
    error?: string | undefined,
    type?: "text" | "email" | "password" | "file",
    getFieldProps: FieldInputProps<any>,
};
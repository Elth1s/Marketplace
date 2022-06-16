export interface IAutocomplete {
    label: string,
    name: string,
    touched?: boolean | undefined,
    error?: string | undefined,
    options: Array<any>,
    getOptionLabel: (option: any) => any,
    isOptionEqualToValue: (option: any, value: any) => boolean,
    defaultValue: any | undefined,
    onChange: (e: any, value: any) => void,
};
import { AutocompleteStyle, TextFieldStyle } from "./style";
import { IAutocomplete } from "./type";

const AutocompleteComponent: React.FC<IAutocomplete> = ({ options, getOptionLabel, isOptionEqualToValue, defaultValue, onChange, label, name, touched, error }) => {
    return (
        <AutocompleteStyle
            options={options}
            getOptionLabel={getOptionLabel}
            isOptionEqualToValue={isOptionEqualToValue}
            defaultValue={defaultValue}
            onChange={onChange}
            renderInput={(params) => (
                <TextFieldStyle
                    {...params}
                    fullWidth
                    variant="standard"
                    label={label}
                    name={name}
                    error={Boolean(touched && error)}
                    helperText={touched && error} />
            )} />
    )
}

export default AutocompleteComponent;
import Autocomplete from '@mui/material/Autocomplete';
import TextField, { TextFieldProps } from '@mui/material/TextField';

import { styled } from '@mui/material/styles';

export const AutocompleteStyle = styled(Autocomplete)(({ theme }) => ({
}));

export const TextFieldStyle = styled(TextField)<TextFieldProps>(({ theme }) => ({
    "& .MuiInputLabel-root": {
        color: theme.palette.mode == "dark" ? "#FFF" : "#000",
        "&.Mui-focused": {
            color: theme.palette.secondary.main
        },
        "&.Mui-error ": {
            color: theme.palette.error.main
        },
    },
    "& .MuiInput-underline:before": {
        borderBottom: `2px solid ${theme.palette.mode == "dark" ? "#FFF" : "#000"}`
    },
    "& .MuiInput-underline:after": {
        borderBottom: `2px solid ${theme.palette.secondary.main}`
    },
    "& .MuiInput-underline:hover:before": {
        borderBottom: "2px solid blue"
    },
    "& .MuiIconButton-root": {
        color: theme.palette.mode == "dark" ? "#FFF" : "#000",
    },
}));
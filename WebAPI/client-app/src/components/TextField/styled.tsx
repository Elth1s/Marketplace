import TextField, { TextFieldProps } from '@mui/material/TextField';
import { styled } from '@mui/material/styles';

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
    "& .MuiInput-underline": {
        ":before": {
            borderBottom: `2px solid ${theme.palette.mode == "dark" ? "#FFF" : "#000"}`
        },
        ":after": {
            borderBottom: `2px solid ${theme.palette.secondary.main}`
        },
    },
    "& .MuiIconButton-root": {
        color: theme.palette.mode == "dark" ? "#FFF" : "#000",
    },
}));

export const TextFieldFirstStyle = styled(TextField)<TextFieldProps>(({ theme }) => ({
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

export const TextFieldSecondStyle = styled(TextField)<TextFieldProps>(({ theme }) => ({
    "& .MuiInputLabel-root": {
        color: theme.palette.mode == "dark" ? "#FFF" : "#000",
        "&.Mui-focused": {
            color: theme.palette.primary.main
        },
        "&.Mui-error ": {
            color: `${theme.palette.error.main}`
        },
        "&.Mui-disabled": {
            color: "#777777"
        },
    },
    "& .MuiOutlinedInput-root": {
        color: theme.palette.mode == "dark" ? "#FFF" : "#000",
        "& fieldset": {
            borderRadius: "10px",
            borderColor: theme.palette.mode == "dark" ? "#FFF" : "#000",
        },
        "&:hover fieldset": {
            color: theme.palette.mode == "dark" ? "#FFF" : "#000",
        },
        "&.Mui-focused fieldset": {
            color: theme.palette.mode == "dark" ? "#FFF" : "#000",
        },
        "&.Mui-disabled fieldset": {
            borderColor: "#7e7e7e"
        },
        "&.Mui-error fieldset": {
            borderColor: `${theme.palette.error.main}`
        },
        "&.Mui-error:hover fieldset": {
            borderColor: `${theme.palette.error.main}`
        }
    },
}));
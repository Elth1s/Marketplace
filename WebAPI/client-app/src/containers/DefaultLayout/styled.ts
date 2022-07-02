import { Box, styled, TextField } from "@mui/material";

export const BoxContainer = styled(Box)(({ theme }) => ({
    width: "1560px",
    height: "100%",
    marginLeft: "auto",
    marginRight: "auto",
}));

export const TextFieldStyle = styled(TextField)(({ theme }) => ({
    width: "660px",
    height: "58px",
    "& fieldset": {
        borderRadius: "9px",
        borderColor: "#7E7E7E",
    },
    InputLabelProps: {
        color: "#7E7E7E"
    },
    inputProps: {
        height: "58px",
        paddingTop: "0px",
        paddingBottom: "0px",
        fontSize: "18px"
    },
    "& .MuiOutlinedInput-root": {
        color: "#777777",
        "& fieldset": {
            borderColor: "#7E7E7E"
        }
    }
}));

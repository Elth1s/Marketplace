import Paper from "@mui/material/Paper";
import Typography from "@mui/material/Typography";

import { styled } from "@mui/material";

export const PaperStyled = styled(Paper)(() => ({
    margin: "20px 0 130px", 
    padding: "24px 16px 46px", 
    boxShadow: "0px 5px 15px rgba(0, 0, 0, 0.25)", 
    borderRadius: "10px"
}));

export const TypographyStyled = styled(Typography)(() => ({
    fontSize: "36px",
    marginBottom: "55px"
}));
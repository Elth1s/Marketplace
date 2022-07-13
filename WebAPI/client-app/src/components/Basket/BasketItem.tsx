import { DeleteOutline } from "@mui/icons-material"
import { Box, IconButton, Typography } from "@mui/material"
import { FC } from "react"

import { empty } from "../../assets/backgrounds"

interface Props {
    id: number
    image: string
    name: string
    price: number
}

const BasketItem: FC<Props> = ({ image, name, price }) => {
    return (
        <Box sx={{ display: "flex", justifyContent: "space-between", alignItems: "center" }}>
            <img
                style={{ width: "90px", height: "90px", objectFit: "contain" }}
                src={image != "" ? image : empty}
                alt="productImage"
            />
            <Box>
                <Typography variant="h6" sx={{ width: "250px" }}>
                    {name}
                </Typography>
            </Box>
            <Box>
                <Typography variant="h4">
                    {price} &#8372;
                </Typography>
            </Box>
            <IconButton
                sx={{ borderRadius: '12px', p: 0.5 }}
                size="large"
                aria-label="search"
                color="inherit"
            >
                <DeleteOutline sx={{ fontSize: "30px" }} />
            </IconButton>
        </Box>
    )
}

export default BasketItem
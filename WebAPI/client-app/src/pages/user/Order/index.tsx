import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";

import { PaperStyled, Img } from "../styled";
import { ListItemStyled, ListItemTextStyled } from "./styled";

const iphone = "https://images.unsplash.com/photo-1607936854279-55e8a4c64888?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=52&q=25"
const product = [
    { number: 650546489, data: '20.13.2001', state: true, price: 26999, image: iphone },
    { number: 650546489, data: '20.13.2001', state: true, price: 26999, image: iphone },
    { number: 650546489, data: '20.13.2001', state: true, price: 26999, image: iphone },
]

const Order = () => {
    return (
        <>
            <Typography variant="h1" color="primary" sx={{ mb: "27px" }}>My order</Typography>
            <PaperStyled sx={{ px: "24px", py: "14px", ml: "33px", mr: "37px" }}>
                {product.map((item, index) => (
                    <ListItemStyled key={index}>
                        <ListItemTextStyled
                            primary={`№${item.number} from ${item.data}`}
                            secondary={`${item.state ? 'Executed' : 'Not executed'}`}
                        />
                        <ListItemTextStyled
                            primary='Price'
                            secondary={`${item.price} hrn`}
                        />
                        <Box sx={{ width: 52, height: 52 }}>
                            <Img alt={`image ${item.number}`} src={item.image} />
                        </Box>
                    </ListItemStyled>
                ))}
            </PaperStyled>
        </>
    );
}

export default Order;
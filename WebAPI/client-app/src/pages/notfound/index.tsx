import Container from '@mui/material/Container';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import { Button } from '@mui/material';

const NotFound = () => {
    return (
        <Box
            sx={{
                display: "flex",
                flexDirection: "column",
                justifyContent: "center",
                minHeight: "100vh",
                textAlign: "center",
            }}
        >
            <Container component="main" sx={{ maxWidth: { xl: "xl", lg: "lg", md: "md" } }}>
                <Typography sx={{ fontSize: "150px", fontWeight: "600", lineHeight: "188px", marginBottom: "20px" }}>404</Typography>
                <Typography variant="h1" sx={{ marginBottom: "20px" }}>Page not found</Typography>
                <Typography variant="h4" sx={{ fontWeight: "500", marginBottom: "95px" }}>
                    Unfortunately, the page you asked for could not be found.
                    <br />
                    Please go back to the main page.
                </Typography>
                <Button variant="contained" href="/">Page home</Button>
            </ Container>
        </Box>
    );
}

export default NotFound;
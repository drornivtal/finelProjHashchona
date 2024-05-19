

import { createTheme } from "@mui/material";



const theme = createTheme({
    components: {
        myInput: {
            styleOverrides: {
                // Set custom styles for the input field
                root: {

                    backgroundColor: 'yellowgreen', // background color
                    border: 'none', // border color

                    borderRadius: '28px', // Set border radius
                    // padding: '8px 12px', // Set padding
                    // fontSize: '20px', // Set font size

                    '& .MuiInputLabel-root': {
                        color: 'green', // Set label color to pink
                    },
                    '& .MuiInput-underline:before': {
                        // borderBottom: 'none', // Disable bottom line
                    },
                    '& .MuiInput-underline:after': {
                        // borderBottom: 'none', // Disable bottom line
                    },
                    '&:hover .MuiInputLabel-root': {
                        color: 'black', // label color on hover
                        paddingTop: '2px',
                        fontWeight: '600',
                    },
                    '&:focus .MuiInputLabel-root': {
                        // color: 'pink', // Set label color to pink when focused

                    },
                },
            },
        },
    },
});

export default theme;

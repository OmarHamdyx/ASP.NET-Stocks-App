module.exports = {
    content: [
        './Views/**/*.{cshtml,razor}',
        "./node_modules/flowbite/**/*.js"],
    darkMode: 'class',
    theme: {
        extend: {},
    },
    plugins: [
        require('tailwindcss-animated')
    ],
}
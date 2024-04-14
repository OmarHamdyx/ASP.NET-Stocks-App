module.exports = {
    content: [
        './Views/**/*.{cshtml,razor}',
        "./node_modules/flowbite/**/*.js"],
    darkMode: 'media',
    theme: {
        extend: {},
    },
    plugins: [
        require('flowbite/plugin')
    ],
}
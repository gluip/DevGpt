    function isElementInvisible(element) {
        var style = window.getComputedStyle(element);
        return style.display === 'none' || style.visibility === 'hidden';
    }
    // Function to check if an element or its ancestors are invisible
    function isElementOrAncestorsInvisible(element) {
        // Check if the element itself is invisible
        if (isElementInvisible(element)) {
            return true;
        }

        // Check if any ancestor is invisible
        var parent = element.parentElement;
        while (parent) {
            if (isElementInvisible(parent)) {
                return true;
            }

            parent = parent.parentElement;
        }

        return false;
    }



    // Function to annotate invisible elements and their ancestors
    function annotateInvisibleElementsAndAncestors() {
        // Get all elements in the document
        var allElements = document.querySelectorAll('*');

        // Loop through each element and check if it or its ancestors are invisible
        allElements.forEach(function (element) {
            if (isElementOrAncestorsInvisible(element)) {
                // Annotate the invisible element with a custom attribute
                element.setAttribute('data-visible', 'false');
            }
            else {
                element.setAttribute('data-visible', 'true');
            }
        });
    }

    // Call the function when the document is fully loaded
    annotateInvisibleElementsAndAncestors();



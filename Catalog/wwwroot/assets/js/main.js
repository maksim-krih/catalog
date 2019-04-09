jQuery(document).ready(function() {
	
	/* ===== Affix Sidebar ===== */
	/* Ref: http://getbootstrap.com/javascript/#affix-examples */

    	
	jQuery('#doc-menu').affix({
        offset: {
            top: (jQuery('#header').outerHeight(true) + jQuery('#doc-header').outerHeight(true)) + 45,
            bottom: (jQuery('#footer').outerHeight(true) + jQuery('#promo-block').outerHeight(true)) + 75
        }
    });
    
    /* Hack related to: https://github.com/twbs/bootstrap/issues/10236 */
    jQuery(window).on('load resize', function() {
        jQuery(window).trigger('scroll'); 
    });

    /* Activate scrollspy menu */
    jQuery('body').scrollspy({target: '#doc-nav', offset: 100});
    
    /* Smooth scrolling */
	jQuery('a.scrollto').on('click', function(e){
        //store hash
        var target = this.hash;    
        e.preventDefault();
		jQuery('body').scrollTo(target, 800, {offset: 0, 'axis':'y'});
		
	});
	
    
    /* ======= jQuery Responsive equal heights plugin ======= */
    /* Ref: https://github.com/liabru/jquery-match-height */
    
     jQuery('#cards-wrapper .item-inner').matchHeight();
     jQuery('#showcase .card').matchHeight();
     
    /* Bootstrap lightbox */
    /* Ref: http://ashleydw.github.io/lightbox/ */

    jQuery(document).delegate('*[data-toggle="lightbox"]', 'click', function(e) {
        e.preventDefault();
        jQuery(this).ekkoLightbox();
    });    


});